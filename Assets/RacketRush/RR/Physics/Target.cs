using System;
using UnityEngine;

namespace RacketRush.RR.Physics
{
    [RequireComponent(typeof(MeshCollider))]
    public class Target : MonoBehaviour
    {
        public int TriangleIndex
        {
            get => _triangleIndex;
            set => _triangleIndex = value;
        }

        private Material _activeMaterial;
        private Material _idleMaterial;
        
        private int _triangleIndex;
        private bool _isActive;
        private MeshRenderer _meshRenderer;
        private Action _onHitSuccess;
        
        public void Populate(int triangleIndex, Material activeMaterial, Material idleMaterial, Action onHitSuccess)
        {
            _triangleIndex = triangleIndex;
            _meshRenderer = GetComponent<MeshRenderer>();
            _activeMaterial = activeMaterial;
            _idleMaterial = idleMaterial;
            _meshRenderer.material = _idleMaterial;
            _onHitSuccess = onHitSuccess;
        }

        public void EnableTarget()
        {
            _isActive = true;
            _meshRenderer.material = _activeMaterial;
        }

        public void DisableTarget()
        {
            _isActive = false;
            _meshRenderer.material = _idleMaterial;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (_isActive)
            {
                _onHitSuccess.Invoke();
                Debug.Log($"Hit triangle {_triangleIndex}");
            }
        }
    }
}
