using Infrastructure.AssetProvider;
using Infrastructure.GameObjectFactory;
using Infrastructure.Input;
using Modules.StateMachine;

namespace Infrastructure.GameStateMachine.States
{
    public class BootstrapState : IParameterlessState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly Services.Services _services;

        public BootstrapState(GameStateMachine stateMachine, Services.Services services)
        {
            _stateMachine = stateMachine;
            _services = services;
        }

        public void Enter()
        {
            RegisterServices();
            EnterLoadMainMenuState();
        }

        public void Exit()
        {
        }

        private void EnterLoadMainMenuState() =>
            _stateMachine.Enter<LoadMainMenuState>();

        private void RegisterServices()
        {
            AssetProvider.AssetProvider assetProvider = new AssetProvider.AssetProvider();
            _services.RegisterSingle<IAssetProvider>(assetProvider);
            GameObjectFactory.GameObjectFactory gameObjectFactory = new GameObjectFactory.GameObjectFactory(assetProvider);
            _services.RegisterSingle<IGameObjectFactory>(gameObjectFactory);
            IInputService inputService = gameObjectFactory.CreateInputService();
            _services.RegisterSingle<IInputService>(inputService);
        }
    }
}