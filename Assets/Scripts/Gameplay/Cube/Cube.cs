using System;
using System.Collections.Generic;
using Infrastructure.GameObjectFactory;
using Model;
using Model.Cube;
using Modules;
using UnityEngine;
using Utility.Extensions;
using Random = UnityEngine.Random;

namespace Gameplay.Cube
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<CubeFaceType, Face> _faces;
        [SerializeField] private MeshFilter _floorMesh;
        [SerializeField] private MeshFilter _wallMesh;

        private List<MeshFilter> _florMeshes = new List<MeshFilter>();
        private List<MeshFilter> _wallMeshes = new List<MeshFilter>();

        public void Initialize(int size, IGameObjectFactory gameObjectFactory)
        {
            Model.Cube.Cube cubeModel = new Model.Cube.Cube(size);
            BackTrackingMazeGenerator generator = new BackTrackingMazeGenerator();
            var randomCell = cubeModel.TypedFaces[CubeFaceType.Front][Random.Range(0, size), Random.Range(0, size)];
            generator.Generate(randomCell);

            foreach (var pair in cubeModel.TypedFaces)
                _faces.Get(pair.Key).Initialize(pair.Value, gameObjectFactory);

            SetStartEnd();
            CollectMeshes();
            DisableCells();
            GenerateCombineMesh();
            GenerateGlassMeshes();
        }

        private void CollectMeshes()
        {
            foreach (var pair in _faces.Dictionary)
            {
                _florMeshes.AddRange(pair.Value.FlorMeshes);
                _wallMeshes.AddRange(pair.Value.WallMeshes);
            }
        }

        private void DisableCells()
        {
            foreach (var pair in _faces.Dictionary)
                pair.Value.DisableCells();
        }

        private void GenerateGlassMeshes()
        {
            foreach (var pair in _faces.Dictionary)
                pair.Value.GenerateGlassMesh();
        }

        private void GenerateCombineMesh()
        {
            Mesh wallMesh = _wallMeshes.ToArray().Combine();
            Mesh floorMesh = _florMeshes.ToArray().Combine();

            _floorMesh.sharedMesh = floorMesh;
            _wallMesh.sharedMesh = wallMesh;
            _floorMesh.GetComponent<MeshCollider>().sharedMesh = floorMesh;
            _wallMesh.GetComponent<MeshCollider>().sharedMesh = wallMesh;

            _floorMesh.gameObject.SetActive(true);
            _wallMesh.gameObject.SetActive(true);
        }

        public Cell Start { get; private set; }
        public Cell End { get; private set; }

        private void SetStartEnd()
        {
            foreach (var pair in _faces.Dictionary)
            {
                if (pair.Value.Start != null)
                    Start = pair.Value.Start;

                if (pair.Value.End != null)
                    End = pair.Value.End;
            }
        }

        public Face GetFaceView(CubeFaceType cubeFaceType) =>
            _faces.Get(cubeFaceType);
    }
}