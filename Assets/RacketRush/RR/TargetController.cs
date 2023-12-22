using System.Collections.Generic;
using UnityEngine;

namespace RacketRush.RR
{
    public class TargetController : MonoBehaviour
    {
        [Tooltip("The points on the dome")]
        [SerializeField] private Transform[] points;
        [SerializeField] private Material targetMaterial;

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

        // Cache target objects in _targets list for later use
        private List<GameObject> _targets = new List<GameObject>();

        private void Start()
        {
            GenerateAndCacheTargets();
        }

        #region Target Creation

        private void GenerateAndCacheTargets()
        {
            for (int i = 0; i < _triangleIndexList.Length; i++)
            {
                GenerateTargetFromPoints(_triangleIndexList[i], i);
            }
        }

        private void GenerateTargetFromPoints(int[] trianglePoints, int triangleIndex)
        {
            var mesh = CreateTargetMesh(trianglePoints, triangleIndex, out var triangleObject);
            PrepareTarget(triangleIndex, triangleObject, mesh);
        }

        private Mesh CreateTargetMesh(int[] trianglePoints, int targetIndex, out GameObject triangleObject)
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
            int[] triangles = new int[] { 0, 1, 2 };

            // Assign vertices and triangles to the mesh
            mesh.vertices = vertices;
            mesh.triangles = triangles;

            // Recalculate normals (important for rendering)
            mesh.RecalculateNormals();

            // Create a new GameObject to display the mesh
            triangleObject = new GameObject($"Triangle_{targetIndex}");
            triangleObject.transform.position = centroid;
            return mesh;
        }
        
        private void PrepareTarget(int triangleIndex, GameObject triangleObject, Mesh mesh)
        {
            // Add necessary components
            MeshFilter meshFilter = triangleObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = triangleObject.AddComponent<MeshRenderer>();
            MeshCollider meshCollider = triangleObject.AddComponent<MeshCollider>();
            Target target = triangleObject.AddComponent<Target>();
            
            triangleObject.layer = LayerMask.NameToLayer("Hittable");
            meshCollider.sharedMesh = mesh;
            meshFilter.mesh = mesh;
            
            target.Populate(triangleIndex);
            meshRenderer.material = targetMaterial;
            _targets.Add(triangleObject);
        }

        #endregion
    }
}
