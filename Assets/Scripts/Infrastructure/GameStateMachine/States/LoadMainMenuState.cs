using Infrastructure.SceneManagement;
using Modules.StateMachine;
using UI;
using UI.Menu;
using UnityEngine;
using Utility.Static.StringNames;

namespace Infrastructure.GameStateMachine.States
{
    public class LoadMainMenuState: IParameterlessState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;

        public LoadMainMenuState(SceneLoader sceneLoader, GameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(SceneNames.MainMenu, OnSceneLoaded);
        }

        public void Exit()
        {
            
        }
        
        private void OnSceneLoaded()
        {
          GameObject.FindObjectOfType<Menu>().Initialize(_gameStateMachine);
          _gameStateMachine.Enter<MenuState>();
        }
    }
}