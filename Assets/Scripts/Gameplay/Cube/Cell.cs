using Model;
using Modules;
using UnityEngine;

namespace Gameplay
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _floorMeshRenderer;
        [SerializeField] private MeshRenderer _anyWallMeshRenderer;
        [SerializeField] private SerializableDictionary<Direction, GameObject> _walls;
        [SerializeField] private SerializableDictionary<Direction, GameObject> _edges;
        [SerializeField] private Material _floorStartMaterial;
        [SerializeField] private Material _floorFinishMaterial;

        public void Initialize(Model.Cell cellModel)
        {
            foreach (var pair in cellModel.Walls)
            {
                _walls.Get(pair.Key).SetActive(pair.Value);

                foreach (Direction direction in cellModel.Edges)
                    _edges.Get(direction).SetActive(true);

                if (cellModel.IsStart)
                    _floorMeshRenderer.material = _floorStartMaterial;

                if (cellModel.IsEnd)
                    _floorMeshRenderer.material = _floorFinishMaterial;
            }
        }

        public float CellSize => _floorMeshRenderer.bounds.size.x - _anyWallMeshRenderer.bounds.size.x;
    }
}