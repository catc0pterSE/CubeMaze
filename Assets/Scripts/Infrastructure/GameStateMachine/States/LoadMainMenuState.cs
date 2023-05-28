using Infrastructure.SceneManagement;
using Modules.StateMachine;

namespace Infrastructure.GameStateMachine.States
{
    public class LoadMainMenuState: IParameterlessState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;
        private readonly Services.Services _services;

        public LoadMainMenuState(SceneLoader sceneLoader, GameStateMachine gameStateMachine, Services.Services services)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _services = services;
        }

        public void Enter()
        {
           
        }

        public void Exit()
        {
            
        }
        
        private void OnSceneLoaded()
        {
          
        }
    }
}