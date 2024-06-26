using System.Collections;
using RacketRush.RR.Utils;
using UnityEngine;

namespace RacketRush.RR.Views.Actors
{
    [RequireComponent(typeof(AudioSource))]
    public class BallView : BaseView
    {
        [SerializeField] [Range(5f, 20f)] private float ballLife;
        [SerializeField] private AudioClip[] ballCollisionSoundClips;
        [SerializeField] [Range(1,5)] private int maxCollisionSound;
        
        protected override bool IsValidComponent
        {
            get
            { 
                if (!ballCollisionSoundClips.HasValidLength(4))
                {
                    return false;
                }

                return base.IsValidComponent;
            }
        }

        private Rigidbody _ballRigidbody;
        private AudioSource _ballAudio;
        private int _collisionSoundCount;

        public Rigidbody BallRigidbody => _ballRigidbody;

        private void Awake()
        {
            _ballRigidbody = GetComponent<Rigidbody>();
            PrepareSoundPlayer();
        }

        public virtual void OnBallSelected()
        {
            gameObject.SetActive(true);
            StartCoroutine(StartLifeTimer());
        }
        
        protected virtual void OnLifeEnded()
        {
            _ballRigidbody.velocity = Vector3.zero;
            gameObject.SetActive(false);
            _collisionSoundCount = 0;
        }

        // Disable the ball after a certain time
        private IEnumerator StartLifeTimer()
        {
            yield return new WaitForSeconds(ballLife);
            OnLifeEnded();   
        }

        private void OnCollisionEnter(Collision collision)
        {
            PlayCollisionSound();
        }

        #region Sound

        private void PrepareSoundPlayer()
        {
            _ballAudio = GetComponent<AudioSource>();
        }

        private void PlayCollisionSound()
        {
            if (_collisionSoundCount >= maxCollisionSound)
            {
                return;
            }

            _ballAudio.clip = ballCollisionSoundClips[_collisionSoundCount];
            _ballAudio.Play();
            _collisionSoundCount++;
        }

        #endregion
    }
}
