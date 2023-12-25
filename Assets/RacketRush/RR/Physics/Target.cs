using UnityEngine;

namespace RacketRush.RR.Physics
{
    [RequireComponent(typeof(MeshCollider))]
    public class Target : MonoBehaviour
    {
        private int _triangleIndex;

        public int TriangleIndex
        {
            get => _triangleIndex;
            set => _triangleIndex = value;
        }
        
        public void Populate(int triangleIndex)
        {
            _triangleIndex = triangleIndex;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"Hit triangle {_triangleIndex}");
        }
    }
}
