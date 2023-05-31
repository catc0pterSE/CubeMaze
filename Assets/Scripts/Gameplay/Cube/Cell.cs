using System.Collections.Generic;
using Model;
using Modules;
using UnityEngine;

namespace Gameplay.Cube
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _floorMeshRenderer;
        [SerializeField] private MeshRenderer _anyWallMeshRenderer;
        [SerializeField] private SerializableDictionary<Direction, GameObject> _walls;
        [SerializeField] private SerializableDictionary<Direction, GameObject> _edges;
        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private EndLevelTrigger _endLevelTrigger;
        [SerializeField] private MeshFilter _florMesh;
        [SerializeField] private MeshFilter[] _wallMeshes;
        [SerializeField] private MeshFilter[] _glassMeshes;
        [SerializeField] private GameObject _startZone;
        [SerializeField] private GameObject _finishZone;

        public bool IsStart { get; private set; }
        public bool IsEnd { get; private set; }

        public MeshFilter FlorMesh => _florMesh;
        public List<MeshFilter> GetWallMeshes => GetEnabledMeshesFrom(_wallMeshes);
        public List<MeshFilter> GetGlassMeshes => GetEnabledMeshesFrom(_glassMeshes);
        public float CellSize => _floorMeshRenderer.bounds.size.x - _anyWallMeshRenderer.bounds.size.x;
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
                    _startZone.gameObject.SetActive(true);
                    IsStart = true;
                }

                if (cellModel.IsEnd)
                {
                    _finishZone.SetActive(true);
                    IsEnd = true;
                    _endLevelTrigger.gameObject.SetActive(true);
                }
            }
        }

        private List<MeshFilter> GetEnabledMeshesFrom(MeshFilter[] meshFilters)
        {
            List<MeshFilter> enabledMeshes = new List<MeshFilter>();

            foreach (var mesh in meshFilters)
            {
                if (mesh.gameObject.activeInHierarchy)
                    enabledMeshes.Add(mesh);
            }

            return enabledMeshes;
        }
    }
}