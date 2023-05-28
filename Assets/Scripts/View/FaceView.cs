using System.Collections.Generic;
using Model;
using UnityEngine;

namespace View
{
    public class FaceView : MonoBehaviour
    {
        [SerializeField] private CellView _cellPrefab;
        private Face _faceModel;
        private List<CellView> _cellViews = new List<CellView>();

        public void Initialize(Face faceModel)
        {
            _faceModel = faceModel;
            int size = faceModel.FaceSize;
            Transform faceTransform = transform;
            Vector3 faceForward = faceTransform.forward;
            faceTransform.position += faceForward*_cellPrefab.CellSize*size/2;

            bool even = size % 2 == 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    CellView cellView = Instantiate(_cellPrefab, transform);
                    Transform cellTransform = cellView.transform;

                    float verticalOffset = (size / 2 - i)*_cellPrefab.CellSize;
                    float horizontalOffset = (size / 2 - j)*_cellPrefab.CellSize;

                    if (even)
                    {
                        verticalOffset -= _cellPrefab.CellSize / 2;
                        horizontalOffset -= _cellPrefab.CellSize / 2;
                    }
                    
                    cellTransform.rotation = faceTransform.rotation;
                    cellTransform.position += cellTransform.up * verticalOffset + cellTransform.right * horizontalOffset;
                    cellView.Initialize(faceModel.GetCellByIndex(i,j));
                    _cellViews.Add(cellView);
                }
            }

        }
    }
}