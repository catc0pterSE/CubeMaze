using System;
using System.Numerics;
using Infrastructure.Services;

namespace Infrastructure.Input
{
    public interface IInputService: IService
    {
        public event Action UpButtonPressed;
        public event Action DownButtonPressed;
        public event Action LeftButtonPressed;
        public event Action RightButtonPressed;

        public bool RotateButtonHeld { get; }
        
        public Vector2 PointerMovement { get; }
    }
}