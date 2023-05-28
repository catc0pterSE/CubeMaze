using UnityEngine;

namespace Infrastructure.AssetProvider
{
    public class AssetProvider : IAssetProvider
    {
        public T Provide<T>(string path) where T: Object
            => Resources.Load<T>(path);

    }
}