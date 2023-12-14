using UnityEngine;

namespace RocketRush.RR.Physics
{
    public class ColliderPlacer : MonoBehaviour
    {
        [SerializeField] private Rigidbody racketRigidbody;
        [SerializeField] [Range(0f,100f)] private float followSpeed;

        private void FixedUpdate()
        {
            Transform refTransform = transform;
            Transform rbTransform = racketRigidbody.transform;
            
            rbTransform.rotation = refTransform.rotation;

            Vector3 motionDir = (refTransform.position - rbTransform.position);
            Vector3 followVelocity = motionDir * followSpeed;

            racketRigidbody.velocity = followVelocity;
        }
    }
}
