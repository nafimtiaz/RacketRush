using Oculus.Haptics;
using RacketRush.RR.Misc;
using UnityEngine;

namespace RacketRush.RR.Physics
{
    [RequireComponent(typeof(AudioSource))]
    public class Racket : MonoBehaviour
    {
        [SerializeField] private Transform detectionCenter;
        [SerializeField] [Range(0.01f,1f)] private float forceMultiplier;
        [SerializeField] private Vector3 detectionBoxHalfExtents;
        [SerializeField] [Range(0f,100f)] private float dynamicRbFollowSpeed;
        [SerializeField] [Range(1,10)] private int ballColliderBufferLength;
        [SerializeField] private PhysicMaterial racketPhysicsMaterial;
        [SerializeField] private LayerMask ballLayer;
        [SerializeField] [Range(0f,1f)] private float stationaryThreshold;
        [SerializeField] [Range(0.3f,0.4f)] private float racketMass;
        [SerializeField] private HapticClip racketHitHapticClip;
        [SerializeField] private AudioClip racketHitSoundClip;

        private Collider[] _colBuffer;
        private Rigidbody _racketDynamicRb;
        private Collider _dynamicRbCollider;
        private HapticClipPlayer _hapticPlayer;
        private AudioSource _racketAudio;

        private bool IsStationary => _racketDynamicRb.velocity.magnitude <= stationaryThreshold;

        private void Awake()
        {
            _colBuffer = new Collider[ballColliderBufferLength];
            CreateDynamicRigidbody();
            PrepareHapticPlayer();
            PrepareSoundPlayer();
        }

        private void FixedUpdate()
        {
            DetectBallAndHit();
            UpdateRacketRigidbody();
            ToggleStaticRigidbody();
        }

        #region Ball Detection

        private void DetectBallAndHit()
        {
            if (IsStationary)
            {
                return;
            }
            
            // When the object is moving fast use overlap box
            // to detect balls instead of default collision
            // Using Non Alloc Overlap box to avoid allocation on each physics update
            var size = UnityEngine.Physics.OverlapBoxNonAlloc(
                detectionCenter.position, 
                detectionBoxHalfExtents, 
                _colBuffer, 
                Quaternion.identity, 
                ballLayer);

            for (int i = 0; i < size; i++)
            {
                // Play racket hit sound and haptic for each hit with a ball
                _hapticPlayer.Play(Controller.Right);
                _racketAudio.Play();
                
                var velocity = _racketDynamicRb.velocity;
                Vector3 forceDir = velocity;
                float effectiveForce = 10f + (velocity.magnitude * forceMultiplier);
                _colBuffer[i].attachedRigidbody.AddForce(forceDir * effectiveForce);
            }
        }

        #endregion

        #region Dynamic Rigidbody

        private void UpdateRacketRigidbody()
        {
            Transform refTransform = detectionCenter;
            Transform rbTransform = _racketDynamicRb.transform;

            // Update the rotation and velocity to match with the racket ref transform
            // This must happen in fixed update
            rbTransform.rotation = refTransform.rotation;
            Vector3 motionDir = (refTransform.position - rbTransform.position);
            Vector3 followVelocity = motionDir * dynamicRbFollowSpeed;

            _racketDynamicRb.velocity = followVelocity;
        }
        
        private void ToggleStaticRigidbody()
        {
            // Enable the collider when the racket is stationary
            _dynamicRbCollider.enabled = IsStationary;
        }

        private void CreateDynamicRigidbody()
        {
            // Create game object and set position and rotation
            GameObject dynamicRb = new GameObject(GameConstants.RACKET_DYN_RB_NAME);
            dynamicRb.transform.position = detectionCenter.position;
            dynamicRb.transform.rotation = detectionCenter.rotation;
            dynamicRb.layer = LayerMask.NameToLayer(GameConstants.LAYER_RACKET);
            
            // Attach rigidbody
            Rigidbody rb = dynamicRb.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.drag = 0f;
            rb.angularDrag = 0f;
            rb.mass = racketMass;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            _racketDynamicRb = rb;

            // Attach collider
            BoxCollider dynamicRbCollider = dynamicRb.AddComponent<BoxCollider>();
            _dynamicRbCollider = dynamicRbCollider;
            dynamicRbCollider.size = detectionBoxHalfExtents;
            _dynamicRbCollider.material = racketPhysicsMaterial;
        }

        #endregion

        #region Sounds And Haptics

        private void PrepareHapticPlayer()
        {
            _hapticPlayer = new HapticClipPlayer(racketHitHapticClip);
            _hapticPlayer.amplitude = 1f;
            _hapticPlayer.isLooping = false;
        }
        
        private void PrepareSoundPlayer()
        {
            _racketAudio = GetComponent<AudioSource>();
            _racketAudio.clip = racketHitSoundClip;
        }

        #endregion
    }
}
