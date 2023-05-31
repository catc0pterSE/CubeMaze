using System.Collections.Generic;
using Infrastructure.GameObjectFactory;
using JetBrains.Annotations;
using UnityEngine;
using Utility.Extensions;

namespace Gameplay.Cube
{
    public class Face : MonoBehaviour
    {
        [SerializeField] private Cell _cellPrefab;
        [SerializeField] private float _cameraOffsetMultiplier;
        [SerializeField] private Face[] _neghbours;
        [SerializeField] private MeshFilter _glassMesh;

        private Transform _transform;
        private List<Cell> _cells = new List<Cell>();
        private List<MeshFilter> _florMeshes = new List<MeshFilter>();
        private List<MeshFilter> _wallMeshes = new List<MeshFilter>();
        private List<MeshFilter> _glassMeshes = new List<MeshFilter>();

        public Vector3 CameraPosition { get; private set; }
        public IReadOnlyCollection<Face> Neigbours => _neghbours;
        public IReadOnlyCollection<MeshFilter> WallMeshes => _wallMeshes;
        public IReadOnlyCollection<MeshFilter> FlorMeshes => _florMeshes;
        [CanBeNull] public Cell Start { get; private set; }
        [CanBeNull] public Cell End { get; private set; }

        public void Initialize(Model.Cube.Face faceModel, IGameObjectFactory gameObjectFactory)
        {
            int cellsCount = faceModel.FaceSize;
            float size = cellsCount * _cellPrefab.CellSize;
            ArrangeCameraPosition(size);
            ArrangeCellViews(cellsCount, size, faceModel, gameObjectFactory);
            CollectMeshes();
        }

        private void Awake() =>
            _transform = transform;

        public void GenerateGlassMesh()
        {
            Mesh glassMesh = _glassMeshes.ToArray().Combine();
            _glassMesh.sharedMesh = glassMesh;
            _glassMesh.GetComponent<BoxCollider>().size = glassMesh.bounds.size;
            _glassMesh.GetComponent<BoxCollider>().center = glassMesh.bounds.center;
            _glassMesh.gameObject.SetActive(true);
        }

        public void DisableCells()
        {
            foreach (Cell cell in _cells)
            {
               if (!cell.IsStart && !cell.IsEnd) 
                   cell.gameObject.SetActive(false);
            }
               
        }

        private void CollectMeshes()
        {
            foreach (Cell cell in _cells)
            {
                _florMeshes.Add(cell.FlorMesh);
                _wallMeshes.AddRange(cell.GetWallMeshes);
                _glassMeshes.AddRange(cell.GetGlassMeshes);
            }
        }

        private void ArrangeCellViews(int cellsCount, float size, Model.Cube.Face faceModel,
            IGameObjectFactory gameObjectFactory)
        {
            for (int i = 0; i < cellsCount; i++)
            {
                for (int j = 0; j < cellsCount; j++)
                {
                    Cell cell = gameObjectFactory.CreateCell(_transform);
                    Transform cellTransform = cell.transform;
                    float verticalOffset = (size - _cellPrefab.CellSize) / 2 - i * _cellPrefab.CellSize;
                    float horizontalOffset = (size - _cellPrefab.CellSize) / 2 - j * _cellPrefab.CellSize;
                    cellTransform.rotation = transform.rotation;

                    cellTransform.position +=
                        cellTransform.up * verticalOffset
                        + cellTransform.right * horizontalOffset
                        + _transform.forward * size / 2;

                    cell.Initialize(faceModel[i, j]);

                    if (cell.IsStart) Start = cell;
                    if (cell.IsEnd) End = cell;

                    _cells.Add(cell);
                }
            }
        }

        private void ArrangeCameraPosition(float size) =>
            CameraPosition = _transform.position + _transform.forward * (size * _cameraOffsetMultiplier);
    }
}