using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagment
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Load(string path)
        {
            return Resources.Load<GameObject>(path);
        }
    }
}