using System;
using Gameplay.Ball;
using Gameplay.Camera;
using Gameplay.Cube;
using Infrastructure.AssetProvider;
using Infrastructure.Input;
using UnityEngine;
using Utility.Static.StringNames;
using Object = UnityEngine.Object;

namespace Infrastructure.GameObjectFactory
{
    public class GameObjectFactory : IGameObjectFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameObjectFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public InputService CreateInputService() =>
            Object.Instantiate(_assetProvider.Provide<InputService>(ResourcePaths.InputServicePath)) ??
            throw NoComponentException(typeof(InputService));

        public Cell CreateCell(Transform parent) =>
            Object.Instantiate(_assetProvider.Provide<Cell>(ResourcePaths.CellPath), parent) ??
            throw NoComponentException(typeof(Cell));

        public Cube CreateCube() =>
            Object.Instantiate(_assetProvider.Provide<Cube>(ResourcePaths.CubePath)) ??
            throw NoComponentException(typeof(Cube));

        public BallGravity CreateBall(Vector3 at) =>
            Object.Instantiate(_assetProvider.Provide<BallGravity>(ResourcePaths.BallPath), at, Quaternion.identity) ??
            throw NoComponentException(typeof(BallGravity));

        public GameplayCamera CreateCamera() =>
            Object.Instantiate(_assetProvider.Provide<GameplayCamera>(ResourcePaths.CameraPath)) ??
            throw NoComponentException(typeof(GameplayCamera));

        private NullReferenceException NoComponentException(Type type) =>
            new NullReferenceException($"No {type} component on prefab");
    }
}