using UnityEngine;

namespace Infastructure.AssetProvider
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Transform parent);
    }
}