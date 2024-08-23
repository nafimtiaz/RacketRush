using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using RacketRush.RR.Logic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RacketRush.RR.Views.Actors
{
    // This class creates and manages the targets
    public class TargetHandlerView : MonoBehaviour
    {
        [Tooltip("The points on the dome")]
        [SerializeField] private Transform[] points;
        [SerializeField] private Material activeMaterial;
        [SerializeField] private Material idleMaterial;
        [SerializeField] private Material disabledMaterial;
        [SerializeField] private Transform targetsParent;
        [SerializeField] private Transform dummiesParent;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip hitSuccessClip;

        // Each int[] represents the indices of the points of triangular targets
        private int[][] _triangleIndexList = new int[][]
        {
            new [] {0,1,2},
            new [] {0,2,3},
            new [] {0,3,4},
            new [] {0,4,5},
            new [] {0,5,1},
            new [] {5,7,1},
            new [] {1,7,8},
            new [] {9,1,8},
            new [] {2,1,9},
            new [] {10,2,9},
            new [] {11,2,10},
            new [] {11,3,2},
            new [] {12,3,11},
            new [] {12,13,3},
            new [] {13,4,3},
            new [] {13,14,4},
            new [] {4,14,15},
            new [] {4,15,5},
            new [] {5,15,6},
            new [] {5,6,7},
            new [] {6,16,17},
            new [] {6,17,7},
            new [] {7,17,18},
            new [] {7,18,8},
            new [] {8,18,19},
            new [] {9,8,19},
            new [] {9,19,20},
            new [] {10,9,20},
            new [] {10,20,21},
            new [] {21,11,10},
            new [] {22,11,21},
            new [] {22,12,11},
            new [] {22,23,12},
            new [] {12,23,13},
            new [] {23,24,13},
            new [] {24,14,13},
            new [] {24,25,14},
            new [] {14,25,15},
            new [] {15,25,16},
            new [] {15,16,6}
        };

        // Some triangles are not targets, they just prevent the ball from going outside
        private int[] _disabledTriangleIndices =
            new[] { 3, 4, 5, 6, 16, 17, 18, 19, 20, 21, 22, 23, 24, 36, 37, 38, 39 };

        // Cache target objects in _targets list for later use
        private List<TargetView> _targets = new List<TargetView>();
        private Sequence _targetSequence;
        private TargetView _currentTargetView;
        private int _lastTargetIndex;
        private List<int> _activeTriangleIndices;
        private Action _onHitSuccess;

        public void Populate(Action onHitSuccess)
        {
            _onHitSuccess = onHitSuccess;
            GenerateAndCacheTargets();
        }
        
        #region Target Creation

        public void GenerateAndCacheTargets()
        {
            if (_activeTriangleIndices != null)
            {
                return;
            }

            _activeTriangleIndices = new List<int>();
            
            for (int i = 0; i < _triangleIndexList.Length; i++)
            {
                bool isDummy = _disabledTriangleIndices.Contains(i);
                
                if (!isDummy)
                {
                    _activeTriangleIndices.Add(i);    
                }
                
                GenerateTargetFromPoints(_triangleIndexList[i], i, isDummy);
            }

            foreach (var i in _disabledTriangleIndices)
            {
                GenerateTargetFromPoints(_triangleIndexList[i], i, true, true);
            }
        }

        private void GenerateTargetFromPoints(int[] trianglePoints, int triangleIndex, bool isDummy = false, bool isReverse = false)
        {
            var mesh = CreateTargetMesh(trianglePoints, triangleIndex, out var triangleObject, isReverse);
            PrepareTarget(triangleIndex, triangleObject, mesh, isDummy);
        }

        private Mesh CreateTargetMesh(
            int[] trianglePoints, 
            int targetIndex, 
            out GameObject triangleObject,
            bool isReverse = false)
        {
            Mesh mesh = new Mesh();

            // Define vertices
            Vector3[] vertices = new Vector3[]
            {
                points[trianglePoints[0]].position,
                points[trianglePoints[1]].position,
                points[trianglePoints[2]].position
            };

            // Calculate centroid
            Vector3 centroid = (vertices[0] + vertices[1] + vertices[2]) / 3f;

            // Translate vertices to make the centroid the origin
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] -= centroid;
            }

            // Define triangles (order of vertices matters for face direction)
            int[] triangles = isReverse ? GameConstants.TRIANGLE_CW : GameConstants.TRIANGLE_CCW;

            // Assign vertices and triangles to the mesh
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            
            // Define UVs (mapping the triangle to a full texture)
            Vector2[] uvs = new Vector2[]
            {
                new Vector2(0, 0),  // Bottom-left corner of the texture
                new Vector2(1, 0),  // Bottom-right corner of the texture
                new Vector2(0.5f, 1)  // Top-center of the texture
            };

            // Assign UVs to the mesh
            mesh.uv = uvs;

            // Recalculate normals (important for rendering)
            mesh.RecalculateNormals();

            // Create a new GameObject to display the mesh
            triangleObject = new GameObject($"Triangle_{targetIndex}");
            triangleObject.transform.position = centroid;
            return mesh;
        }
        
        private void PrepareTarget(int triangleIndex, GameObject triangleObject, Mesh mesh, bool isDummy = false)
        {
            MeshFilter meshFilter = triangleObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = triangleObject.AddComponent<MeshRenderer>();
            meshFilter.mesh = mesh;
            meshRenderer.material = GetTargetMaterial(triangleIndex);
            MeshCollider meshCollider = triangleObject.AddComponent<MeshCollider>();
            triangleObject.layer = LayerMask.NameToLayer("Hittable");

            if (!isDummy)
            {
                TargetView targetView = triangleObject.AddComponent<TargetView>();
                meshCollider.sharedMesh = mesh;
                targetView.Populate(this, triangleIndex, activeMaterial, idleMaterial, _onHitSuccess);
                _targets.Add(targetView);
                triangleObject.transform.SetParent(targetsParent);
            }
            else
            {
                triangleObject.transform.SetParent(dummiesParent);
            }
        }

        private Material GetTargetMaterial(int triangleIndex)
        {
            if (_disabledTriangleIndices.Contains(triangleIndex))
            {
                return disabledMaterial;
            }

            return idleMaterial;
        }

        #endregion

        #region Target Toggle

        public void StartTargetGeneration()
        {
            _targetSequence = DOTween.Sequence();
            _targetSequence.AppendCallback(() =>
            {
                if (_currentTargetView != null)
                {
                    _currentTargetView.DisableTarget();    
                }
            });
            _targetSequence.AppendCallback(() =>
            {
                int triangleIndex = Random.Range(0, _activeTriangleIndices.Count - 1);
                _targets[triangleIndex].EnableTarget();
                _currentTargetView = _targets[triangleIndex];
            });
            _targetSequence.AppendInterval(3f);
            _targetSequence.SetLoops(-1);
        }

        public void StopTargetGeneration()
        {
            if (_targetSequence != null)
            {
                _targetSequence.Kill();
            }
        }

        #endregion

        #region Post Target Hit

        public void OnTargetHit()
        {
            audioSource.clip = hitSuccessClip;
            audioSource.Play();
        }

        #endregion

        private void OnDestroy()
        {
            if (_targetSequence != null)
            {
                _targetSequence.Kill();
                _targetSequence = null;
            }
        }
    }
}
