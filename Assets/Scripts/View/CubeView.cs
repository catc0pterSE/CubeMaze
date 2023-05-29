using Model;
using Modules;
using test;
using UnityEngine;
using Random = UnityEngine.Random;

namespace View
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField] private int _size;
        [SerializeField] private SerializableDictionary<CubeFaceType, FaceView> _faces;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            Cube cubeModel = new Cube(_size);
            BackTrackingMazeGenerator generator = new BackTrackingMazeGenerator();
            var randomCell = cubeModel.TypedFaces[CubeFaceType.Front][Random.Range(0, _size), Random.Range(0, _size)];
            generator.Generate(randomCell);
            
            foreach (var pair in cubeModel.TypedFaces)
                _faces.Get(pair.Key).Initialize(pair.Value);
        }

        public FaceView GetFaceView(CubeFaceType cubeFaceType) =>
            _faces.Get(cubeFaceType);
    }
}