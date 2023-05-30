using Infrastructure.Input;
using UnityEngine;

namespace Gameplay.Camera
{
    public class CameraRotator: MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 3;
        
        private IInputService _inputService;

        public void Initialize(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        void Update()
        {
            if (_inputService.RotateButtonHeld)
            {
                float rotation = _inputService.PointerMovement.X;
                Vector3 currentRotation = transform.rotation.eulerAngles;
                currentRotation.z += rotation*_rotationSpeed;
                transform.rotation = Quaternion.Euler(currentRotation);
            }
        }
        
    }
}