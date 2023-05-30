using System;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Infrastructure.Input
{
    public class InputService : MonoBehaviour, IInputService
    {
        public const string XMouseAxisName = "Mouse X";
        public const string YMouseAxisName = "Mouse Y";

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public bool RotateButtonHeld => UnityEngine.Input.GetMouseButton(1);

        public Vector2 PointerMovement =>
            new Vector2(UnityEngine.Input.GetAxis(XMouseAxisName), UnityEngine.Input.GetAxis(YMouseAxisName));
        
        public event Action UpButtonPressed;
        public event Action DownButtonPressed;
        public event Action LeftButtonPressed;
        public event Action RightButtonPressed;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.W)) UpButtonPressed?.Invoke();
            if (UnityEngine.Input.GetKeyDown(KeyCode.S)) DownButtonPressed?.Invoke();
            if (UnityEngine.Input.GetKeyDown(KeyCode.A)) LeftButtonPressed?.Invoke();
            if (UnityEngine.Input.GetKeyDown(KeyCode.D)) RightButtonPressed?.Invoke();
        }
    }
}