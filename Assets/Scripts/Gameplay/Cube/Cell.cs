using Model;
using Modules;
using UnityEngine;
using Utility.Extensions;

namespace Gameplay.Cube
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _floorMeshRenderer;
        [SerializeField] private MeshRenderer _anyWallMeshRenderer;
        [SerializeField] private SerializableDictionary<Direction, GameObject> _walls;
        [SerializeField] private SerializableDictionary<Direction, GameObject> _edges;
        [SerializeField] private Material _floorStartMaterial;
        [SerializeField] private Material _floorFinishMaterial;
        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private EndLevelTrigger _endLevelTrigger;

        public bool IsStart { get; private set; }
        public bool IsEnd { get; private set; }

        public Vector3 BallSpawnPoint => _ballSpawnPoint.position;
        public EndLevelTrigger EndLevelTrigger => _endLevelTrigger;
        
        public void Initialize(Model.Cube.Cell cellModel)
        {
            foreach (var pair in cellModel.Walls)
            {
                _walls.Get(pair.Key).SetActive(pair.Value);

                foreach (Direction direction in cellModel.Edges)
                    _edges.Get(direction).SetActive(true);

                if (cellModel.IsStart)
                {
                    _floorMeshRenderer.material = _floorStartMaterial;
                    IsStart = true;
                }

                if (cellModel.IsEnd)
                {
                    _floorMeshRenderer.material = _floorFinishMaterial;
                    IsEnd = true;
                    _endLevelTrigger.gameObject.SetActive(true);
                }
            }
        }

        public float CellSize => _floorMeshRenderer.bounds.size.x - _anyWallMeshRenderer.bounds.size.x;
    }
}