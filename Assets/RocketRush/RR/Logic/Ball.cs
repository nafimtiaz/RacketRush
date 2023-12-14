using System;
using System.Collections;
using UnityEngine;

namespace RocketRush.RR.Logic
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] [Range(5f, 20f)] private float ballLife;

        private Rigidbody _ballRigidbody;

        public Rigidbody BallRigidbody => _ballRigidbody;

        private void Awake()
        {
            _ballRigidbody = GetComponent<Rigidbody>();
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
        }

        // Disable the ball after a certain time
        private IEnumerator StartLifeTimer()
        {
            yield return new WaitForSeconds(ballLife);
            OnLifeEnded();   
        }
    }
}
