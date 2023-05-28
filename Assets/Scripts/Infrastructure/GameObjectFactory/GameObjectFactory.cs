using Infrastructure.AssetProvider;

namespace Infrastructure.GameObjectFactory
{
    public class GameObjectFactory : IGameObjectFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameObjectFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
    }
}