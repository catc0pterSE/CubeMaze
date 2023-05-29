using System.Collections.Generic;
using Model;
using test;
using UnityEngine;

namespace View
{
    public class FaceView : MonoBehaviour
    {
        [SerializeField] private CellView _cellPrefab;
        [SerializeField] private Transform _cameraPosition;
        [SerializeField] private float _cameraOffsetMultiplier;
        private List<CellView> _cellViews = new List<CellView>();

        public float Size { get; private set; }
        
        public CameraPlace CameraPlace => GetComponentInChildren<CameraPlace>();

        public void Initialize(Face faceModel)
        {
            int size = faceModel.FaceSize;
            Transform faceTransform = transform;
            Vector3 faceForward = faceTransform.forward;
            Size = size * _cellPrefab.CellSize;
            _cameraPosition.position = faceTransform.position + faceForward * (Size * _cameraOffsetMultiplier);
            faceTransform.position += faceForward * Size / 2;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    CellView cellView = Instantiate(_cellPrefab, transform);
                    Transform cellTransform = cellView.transform;
                    float verticalOffset = (Size- _cellPrefab.CellSize) / 2 - i * _cellPrefab.CellSize;
                    float horizontalOffset = (Size - _cellPrefab.CellSize) / 2 - j * _cellPrefab.CellSize;
                    cellTransform.rotation = faceTransform.rotation;
                    cellTransform.position += cellTransform.up * verticalOffset + cellTransform.right * horizontalOffset;
                    cellView.Initialize(faceModel[i, j]);
                    _cellViews.Add(cellView);
                }
            }
        }
    }
}