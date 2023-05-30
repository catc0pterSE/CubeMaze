using Gameplay.Ball;
using Gameplay.Camera;
using Gameplay.Cube;
using Infrastructure.GameObjectFactory;
using Infrastructure.Input;
using Infrastructure.SceneManagement;
using Modules.StateMachine;
using UI;
using UnityEngine;
using Utility.Static.StringNames;

namespace Infrastructure.GameStateMachine.States
{
    public class LoadLevelState : IParameterState<int>
    {
        private readonly Services.Services _services;
        private readonly GameStateMachine _gameStateMachine;

        private SceneLoader _sceneLoader;
        private int _cubeSize;

        public LoadLevelState(SceneLoader sceneLoader, Services.Services services, GameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _services = services;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(int cubeSize)
        {
            _cubeSize = cubeSize;
            _sceneLoader.LoadScene(SceneNames.Level, OnSceneLoaded);
        }

        public void Exit()
        {
        }

        private void OnSceneLoaded()
        {
            IGameObjectFactory gameObjectFactory = _services.Single<IGameObjectFactory>();
            IInputService inputService = _services.Single<IInputService>();
            Cube cube = gameObjectFactory.CreateCube();
            cube.Initialize(_cubeSize, gameObjectFactory);
            GameplayCamera camera = gameObjectFactory.CreateCamera();
            BallGravity ball = gameObjectFactory.CreateBall(cube.Start.BallSpawnPoint);
            ball.Initialize(camera.transform);
            camera.Initialize(cube, inputService);
            EndLevelMenu endLevelMenu = GameObject.FindObjectOfType<EndLevelMenu>(true);
            Debug.Log(endLevelMenu == null);
            endLevelMenu.Initialize(cube.End.EndLevelTrigger, camera, _gameStateMachine);
            _gameStateMachine.Enter<GameplayLoopState>();
        }
    }
}