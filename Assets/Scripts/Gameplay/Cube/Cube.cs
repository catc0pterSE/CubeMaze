using Model;
using Model.Cube;
using Modules;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Cube
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private int _size;
        [SerializeField] private SerializableDictionary<CubeFaceType, Face> _faces;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            Model.Cube.Cube cubeModel = new Model.Cube.Cube(_size);
            BackTrackingMazeGenerator generator = new BackTrackingMazeGenerator();
            var randomCell = cubeModel.TypedFaces[CubeFaceType.Front][Random.Range(0, _size), Random.Range(0, _size)];
            generator.Generate(randomCell);
            
            foreach (var pair in cubeModel.TypedFaces)
                _faces.Get(pair.Key).Initialize(pair.Value);
        }

        public Face GetFaceView(CubeFaceType cubeFaceType) =>
            _faces.Get(cubeFaceType);
    }
}