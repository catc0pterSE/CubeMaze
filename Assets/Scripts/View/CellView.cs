using Model;
using Modules;
using UnityEngine;

namespace View
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _florMesh;
        [SerializeField] private MeshRenderer _anyWallMesh;
        [SerializeField] private SerializableDictionary<Direction, GameObject> _walls;
     

        public void Initialize(Cell cellModel)
        {
            foreach (var pair in cellModel.Walls)
            {
                _walls.Get(pair.Key).SetActive(pair.Value);
            }
        }
        
        public float CellSize => _florMesh.bounds.size.x-_anyWallMesh.bounds.size.x;

    }
}