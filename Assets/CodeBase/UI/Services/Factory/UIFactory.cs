using CodeBase.Infrastructure.AssetManagment;
using CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IInstantiator _instantiator;
        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData,  IInstantiator instantiator)
        {
            _assets = assets;
            _staticData = staticData;
            _instantiator = instantiator;
        }

      
        public void CreateUIRoot()
        {
            var uiRootPrefab = _assets.Load(AssetAdress.UIRoot).transform;
            _uiRoot = _instantiator.InstantiatePrefabForComponent<Transform>(uiRootPrefab.transform);
        }

        public void CreateWinWindow()
        {
            
        }

        public void CreateLoseWindow()
        {
            throw new System.NotImplementedException();
        }

    }
}