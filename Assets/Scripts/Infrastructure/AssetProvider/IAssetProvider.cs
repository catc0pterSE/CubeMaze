using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetProvider
{
    public interface IAssetProvider : IService
    {
        public T Provide<T>(string path) where T : Object;
    }
}