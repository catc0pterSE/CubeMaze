using System;
using Gameplay.Camera;
using Gameplay.Cube;
using Infrastructure.GameStateMachine;
using Infrastructure.GameStateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Utility.Extensions;

namespace UI
{
    public class EndLevelMenu: MonoBehaviour
    {
        [SerializeField] private GameObject _hud;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _exitButton;
        
        private EndLevelTrigger _endLevelTrigger;
        private GameplayCamera _gameplayCamera;
        private GameStateMachine _gameStateMachine;

        public void Initialize(EndLevelTrigger endLevelTrigger, GameplayCamera gameplayCamera, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _gameplayCamera = gameplayCamera;
            _endLevelTrigger = endLevelTrigger;
            SubscribeOnTrigger();
            SubscribeOnButtons();
        }

        private void OnDestroy()
        {
            UnsubscribeFromTrigger();
            UnsubscribeFromButtons();
        }

        private void SubscribeOnButtons()
        {
            _menuButton.onClick.AddListener(()=> _gameStateMachine.Enter<LoadMainMenuState>());
            _exitButton.onClick.AddListener(()=> _gameStateMachine.Enter<CloseGameState>());
        }
        
        private void UnsubscribeFromButtons()
        {
            _menuButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        private void SubscribeOnTrigger()
        {
            _endLevelTrigger.BallReachedEnd += Display;
        }
        
        private void UnsubscribeFromTrigger()
        {
            _endLevelTrigger.BallReachedEnd -= Display;
        }

        private void Display()
        {
            _hud.SetActive(false);
            _gameplayCamera.Stop();
            this.EnableObject();
        }
    }
}