using System;
using UnityEngine;

namespace GameLogic.WavesLogic
{
    public class Waves : MonoBehaviour
    {
        [SerializeField] private Vector2Int _dimension = new Vector2Int(10, 10);
        [SerializeField] private float UVScale = 2f;
        [SerializeField] private Octave[] Octaves;
        
        protected MeshFilter MeshFilter;
        protected Mesh Mesh;
        
        void Start()
        {
            Mesh = new Mesh();
            
            Mesh.name = gameObject.name;

            Mesh.vertices = GenerateVerts();
            Mesh.triangles = GenerateTries();
            Mesh.uv = GenerateUVs();
            Mesh.RecalculateNormals();
            Mesh.RecalculateBounds();

            MeshFilter = gameObject.GetComponent<MeshFilter>();
            MeshFilter.mesh = Mesh;
        }
   
        public float GetHeight(Vector3 position)
        {
            var scale = new Vector3(1 / transform.lossyScale.x, 0, 1 / transform.lossyScale.z);
            var localPos = Vector3.Scale((position - transform.position), scale);
            
            var p1 = new Vector3(Mathf.Floor(localPos.x), 0, Mathf.Floor(localPos.z));
            var p2 = new Vector3(Mathf.Floor(localPos.x), 0, Mathf.Ceil(localPos.z));
            var p3 = new Vector3(Mathf.Ceil(localPos.x), 0, Mathf.Floor(localPos.z));
            var p4 = new Vector3(Mathf.Ceil(localPos.x), 0, Mathf.Ceil(localPos.z));
            
            p1.x = Mathf.Clamp(p1.x, 0, _dimension.x);
            p1.z = Mathf.Clamp(p1.z, 0, _dimension.x);
            p2.x = Mathf.Clamp(p2.x, 0, _dimension.x);
            p2.z = Mathf.Clamp(p2.z, 0, _dimension.x);
            p3.x = Mathf.Clamp(p3.x, 0, _dimension.x);
            p3.z = Mathf.Clamp(p3.z, 0, _dimension.x);
            p4.x = Mathf.Clamp(p4.x, 0, _dimension.x);
            p4.z = Mathf.Clamp(p4.z, 0, _dimension.x);
            
            var max = Mathf.Max(Vector3.Distance(p1, localPos), Vector3.Distance(p2, localPos), Vector3.Distance(p3, localPos), Vector3.Distance(p4, localPos) + Mathf.Epsilon);
            var dist = (max - Vector3.Distance(p1, localPos))
                       + (max - Vector3.Distance(p2, localPos))
                       + (max - Vector3.Distance(p3, localPos))
                       + (max - Vector3.Distance(p4, localPos) + Mathf.Epsilon);
            
            var height = Mesh.vertices[index(p1.x, p1.z)].y * (max - Vector3.Distance(p1, localPos))
                         + Mesh.vertices[index(p2.x, p2.z)].y * (max - Vector3.Distance(p2, localPos))
                         + Mesh.vertices[index(p3.x, p3.z)].y * (max - Vector3.Distance(p3, localPos))
                         + Mesh.vertices[index(p4.x, p4.z)].y * (max - Vector3.Distance(p4, localPos));

            return height * transform.lossyScale.y / dist;

        }

        private Vector3[] GenerateVerts()
        {
            var verts = new Vector3[(_dimension.x + 1) * (_dimension.y + 1)];
            
            Vector3 centerPoint = new Vector3(_dimension.x / 2f, 0, _dimension.y / 2f);

            for (int x = 0; x <= _dimension.x; x++)
            {
                for (int z = 0; z <= _dimension.y; z++)
                {
                    verts[index(x, z)] = new Vector3(x - centerPoint.x, 0, z - centerPoint.z);
                }
            }

            return verts;
        }

        private int[] GenerateTries()
        {
            var tries = new int[Mesh.vertices.Length * 6];
            for(int x = 0; x < _dimension.x; x++)
            {
                for(int z = 0; z < _dimension.y; z++)
                {
                    tries[index(x, z) * 6 + 0] = index(x, z);
                    tries[index(x, z) * 6 + 1] = index(x + 1, z + 1);
                    tries[index(x, z) * 6 + 2] = index(x + 1, z);
                    tries[index(x, z) * 6 + 3] = index(x, z);
                    tries[index(x, z) * 6 + 4] = index(x, z + 1);
                    tries[index(x, z) * 6 + 5] = index(x + 1, z + 1);
                }
            }

            return tries;
        }

        private Vector2[] GenerateUVs()
        {
            var uvs = new Vector2[Mesh.vertices.Length];
            
            for (int x = 0; x <= _dimension.x; x++)
            {
                for (int z = 0; z <= _dimension.y; z++)
                {
                    var vec = new Vector2((x / UVScale) % 2, (z / UVScale) % 2);
                    uvs[index(x, z)] = new Vector2(vec.x <= 1 ? vec.x : 2 - vec.x, vec.y <= 1 ? vec.y : 2 - vec.y);
                }
            }

            return uvs;
        }

        private int index(int x, int z)
        {
            return x * (_dimension.x + 1) + z;
        }

        private int index(float x, float z)
        {
            return index((int)x, (int)z);
        }
        
        void Update()
        {
            var verts = Mesh.vertices;
            Vector3 offset = new Vector3(-_dimension.x / 2f, 0, -_dimension.y / 2f);
            for (int x = 0; x <= _dimension.x; x++)
            {
                for (int z = 0; z <= _dimension.y; z++)
                {
                    var y = 0f;
                    for (int o = 0; o < Octaves.Length; o++)
                    {
                        if (Octaves[o].alternate)
                        {
                            var perl = Mathf.PerlinNoise((x * Octaves[o].scale.x) / _dimension.x, (z * Octaves[o].scale.y) / _dimension.y) * Mathf.PI * 2f;
                            y += Mathf.Cos(perl + Octaves[o].speed.magnitude * Time.time) * Octaves[o].height;
                        }
                        else
                        {
                            var perl = Mathf.PerlinNoise((x * Octaves[o].scale.x + Time.time * Octaves[o].speed.x) / _dimension.x, (z * Octaves[o].scale.y + Time.time * Octaves[o].speed.y) / _dimension.y) - 0.5f;
                            y += perl * Octaves[o].height;
                        }
                    }
                    verts[index(x, z)] = new Vector3(x + offset.x, y, z + offset.z);
                }
            }
            Mesh.vertices = verts;
            Mesh.RecalculateNormals();
        }

        [Serializable]
        public struct Octave
        {
            public Vector2 speed;
            public Vector2 scale;
            public float height;
            public bool alternate;
        }
    }
}
