using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace RacketRush.RR.Views.Actors
{
    [RequireComponent(typeof(AudioSource))]
    public class BallThrowerView : BaseView
    {
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] [Range(1f,5f)] private float ballThrowInterval;
        [SerializeField] [Range(5f,100f)] private float ballPoolSize;
        [SerializeField] private Transform ballOrigin;
        [SerializeField] [Range(1f, 100f)] private float throwSpeed;
        [SerializeField] private AudioClip ballThrowSoundClip;
        [SerializeField] private Animator ballThrowerPlatformAnimator;
        [SerializeField] private Transform ballThrowerDeviceParent;
        [SerializeField] private Transform ballThrowerLookAtPoint;

        protected override bool IsValidComponent
        {
            get
            { 
                if (ballPrefab == null ||
                    ballOrigin == null ||
                    ballThrowSoundClip == null ||
                    ballThrowerPlatformAnimator == null ||
                    ballThrowerLookAtPoint == null ||
                    ballThrowerDeviceParent == null ||
                    ballThrowerLookAtPoint == null)
                {
                    return false;
                }

                return base.IsValidComponent;
            }
        }

        private Sequence _throwSequence;
        private List<BallView> _ballPool;
        private AudioSource _ballThrowerAudio;
        private Action _onBallThrow;
        private bool _isActive;

        public void Populate(Action onBallThrow)
        {
            _onBallThrow = onBallThrow;
            PrepareSoundPlayer();
            CreateBallPool();
            StartBallThrowSequence();
        }

        private void Update()
        {
            if (_isActive)
            {
                ballThrowerDeviceParent.LookAt(ballThrowerLookAtPoint);
            }
        }

        // Create a pool of balls on start
        private void CreateBallPool()
        {
            // no need to create pool if its already populated
            if (_ballPool != null && _ballPool.Count >= ballPoolSize)
            {
                return;
            }
            
            _ballPool = new List<BallView>();
        
            for (int ballCount = 0; ballCount < ballPoolSize; ballCount++)
            {
                var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
                _ballPool.Add(ball.GetComponent<BallView>());
                ball.SetActive(false);
            }
        }

        // This sequence will throw balls at a time interval
        private void StartBallThrowSequence()
        {
            _isActive = true;
            _throwSequence = DOTween.Sequence();
            _throwSequence.AppendInterval(ballThrowInterval);
            _throwSequence.AppendCallback(() =>
            {
                ToggleThrowerPlatformMovement(true);
                SelectAndThrowBall();
            });
            _throwSequence.SetLoops(-1);
        }

        private void ToggleThrowerPlatformMovement(bool isActive)
        {
            ballThrowerPlatformAnimator.SetBool("IsActive", isActive);
        }
        
        public void StopBallThrowSequence()
        {
            _isActive = false;
            
            if (_throwSequence != null)
            {
                _throwSequence.Kill();
                _throwSequence = null;
            }
            
            ToggleThrowerPlatformMovement(false);
        }

        private BallView GetBallFromPool()
        {
            BallView selectedBall = null;
        
            foreach (var ball in _ballPool)
            {
                if (!ball.gameObject.activeInHierarchy)
                {
                    selectedBall = ball;
                    break;
                }
            }

            return selectedBall;
        }

        private void SelectAndThrowBall()
        {
            BallView selectedBall = GetBallFromPool();
            
            if (selectedBall != null)
            {
                selectedBall.transform.position = ballOrigin.position;
                selectedBall.OnBallSelected();
                selectedBall.BallRigidbody.AddForce(ballOrigin.forward * throwSpeed);
                _onBallThrow.Invoke();
                _ballThrowerAudio.Play();
            }
            else
            {
                Debug.LogWarning("No ball available in pool for throwing!");
            }
        }
        
        private void PrepareSoundPlayer()
        {
            _ballThrowerAudio = GetComponent<AudioSource>();
            _ballThrowerAudio.clip = ballThrowSoundClip;
        }
    }
}
