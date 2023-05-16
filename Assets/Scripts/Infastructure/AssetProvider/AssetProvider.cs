using UnityEngine;

namespace Infastructure.AssetProvider
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path) => 
            Object.Instantiate(Resources.Load<GameObject>(path));
        
        public GameObject Instantiate(string path, Transform parent) => 
            Object.Instantiate(Resources.Load<GameObject>(path), parent);

    }
}