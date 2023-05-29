using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Cube
{
    public class Face : MonoBehaviour
    {
        [SerializeField] private Cell _cellPrefab;
        [SerializeField] private float _cameraOffsetMultiplier;
        [SerializeField] private Face[] _neghbours;

        private Transform _transform;

        public Vector3 CameraPosition { get; private set; }
        public IReadOnlyCollection<Face> Neigbours => _neghbours;

        public void Initialize(Model.Cube.Face faceModel)
        {
            int cellsCount = faceModel.FaceSize;
            float size = cellsCount * _cellPrefab.CellSize;
            ArrangeSelf(size);
            ArrangeCameraPosition(size);
            ArrangeCellViews(cellsCount, size, faceModel);
        }

        private void Awake() =>
            _transform = transform;

        private void ArrangeCellViews(int cellsCount, float size, Model.Cube.Face faceModel)
        {
            for (int i = 0; i < cellsCount; i++)
            {
                for (int j = 0; j < cellsCount; j++)
                {
                    Cell cell = Instantiate(_cellPrefab, transform);
                    Transform cellTransform = cell.transform;
                    float verticalOffset = (size - _cellPrefab.CellSize) / 2 - i * _cellPrefab.CellSize;
                    float horizontalOffset = (size - _cellPrefab.CellSize) / 2 - j * _cellPrefab.CellSize;
                    cellTransform.rotation = transform.rotation;
                    cellTransform.position +=
                        cellTransform.up * verticalOffset + cellTransform.right * horizontalOffset;
                    cell.Initialize(faceModel[i, j]);
                }
            }
        }

        private void ArrangeSelf(float size) =>
            _transform.position += _transform.forward * size / 2;

        private void ArrangeCameraPosition(float size) =>
            CameraPosition = _transform.position + _transform.forward * (size * _cameraOffsetMultiplier);
    }
}