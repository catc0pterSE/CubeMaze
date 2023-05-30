using Modules.StateMachine;
using UnityEngine;

namespace Infrastructure.GameStateMachine.States
{
    public class CloseGameState : IParameterlessState
    {
        private readonly Services.Services _services;

        public CloseGameState(Services.Services services)
        {
            _services = services;
        }

        public void Enter()
        {
            Application.Quit();
        }

        public void Exit()
        {
        }
    }
}