using System;
using System.Linq;
using Infrastructure.AssetProvider;
using Infrastructure.GameObjectFactory;
using Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace View
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField] private int _size;
        [SerializeField] private FaceView[] _faces;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            Cube cubeModel = new Cube(_size);

            BackTrackingMazeGenerator generator = new BackTrackingMazeGenerator();
            var randomCell = cubeModel.Faces.ToArray()[Random.Range(0, cubeModel.Faces.Count)].GetCellByIndex(0, 0);
            generator.Generate(randomCell);

            for (int i = 0; i < cubeModel.Faces.Count; i++)
            {
                _faces[i].Initialize(cubeModel.Faces.ToArray()[i]);
            }
        }
    }
}