using Infrastructure.SceneManagement;
using Modules.StateMachine;

namespace Infrastructure.GameStateMachine.States
{
    public class LoadLevelState : IParameterState<int>
    {
        private readonly Services.Services _services;
        private readonly GameStateMachine _gameStateMachine;

        private int _levelNumber;
        private SceneLoader _sceneLoader;

        public LoadLevelState(SceneLoader sceneLoader, Services.Services services, GameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _services = services;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(int levelNumber)
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