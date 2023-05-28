using System;
using Infrastructure.GameStateMachine.States;
using Infrastructure.SceneManagement;
using UI.Loading;
using UnityEngine;

namespace Infrastructure.EntryPoint
{
    public class GameBootStrapper : MonoBehaviour, IApplicationQuitHandler
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        private GameStateMachine.GameStateMachine _stateMachine;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            
            _loadingCurtain.Show();

            _stateMachine = new GameStateMachine.GameStateMachine(
                new SceneLoader(_loadingCurtain),
                Services.Services.Container, 
                this
            );

            DontDestroyOnLoad(this);
            _stateMachine.Enter<BootstrapState>();
        }

        public event Action ApplicationQuit;

        private void OnApplicationQuit() => ApplicationQuit?.Invoke();
    }
}