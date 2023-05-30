using Infrastructure.Input;
using UnityEngine;
using Utility.Extensions;

namespace Gameplay.Camera
{
    public class GameplayCamera: MonoBehaviour
    {
        [SerializeField] private CameraMover _cameraMover;
        [SerializeField] private CameraRotator _cameraRotator;

        public void Initialize(Cube.Cube cube, IInputService inputService)
        {
            _cameraMover.Initialize(cube, inputService);
            _cameraRotator.Initialize(inputService);
        }

        public void Stop()
        {
           _cameraMover.DisableComponent();
           _cameraRotator.DisableComponent();
        }
    }
}