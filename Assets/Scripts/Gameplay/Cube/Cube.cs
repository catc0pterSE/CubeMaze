using Infrastructure.GameObjectFactory;
using Model;
using Model.Cube;
using Modules;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Cube
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<CubeFaceType, Face> _faces;

        public void Initialize(int size, IGameObjectFactory gameObjectFactory)
        {
            Model.Cube.Cube cubeModel = new Model.Cube.Cube(size);
            BackTrackingMazeGenerator generator = new BackTrackingMazeGenerator();
            var randomCell = cubeModel.TypedFaces[CubeFaceType.Front][Random.Range(0, size), Random.Range(0, size)];
            generator.Generate(randomCell);

            foreach (var pair in cubeModel.TypedFaces)
                _faces.Get(pair.Key).Initialize(pair.Value, gameObjectFactory);
            
            SetStartEnd();
        }

        public Cell Start { get; private set; }
        public Cell End { get; private set; }

        private void SetStartEnd()
        {
            foreach (var pair in _faces.Dictionary)
            {
                if (pair.Value.Start != null)
                    Start = pair.Value.Start;

                if (pair.Value.End != null)
                    End = pair.Value.End;
            }
        }

        public Face GetFaceView(CubeFaceType cubeFaceType) =>
            _faces.Get(cubeFaceType);
    }
}