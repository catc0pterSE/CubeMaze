using Gameplay.Ball;
using Gameplay.Camera;
using Infrastructure.Input;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.GameObjectFactory
{
    public interface IGameObjectFactory : IService
    {
        public InputService CreateInputService();
        public Gameplay.Cube.Cell CreateCell(Transform parent);

        public Gameplay.Cube.Cube CreateCube();

        public BallGravity CreateBall(Vector3 at);

        public GameplayCamera CreateCamera();
    }
}