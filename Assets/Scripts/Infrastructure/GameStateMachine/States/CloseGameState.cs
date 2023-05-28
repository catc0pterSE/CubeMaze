using Modules.StateMachine;

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
          
        }
        
        public void Exit() {}
    }
}