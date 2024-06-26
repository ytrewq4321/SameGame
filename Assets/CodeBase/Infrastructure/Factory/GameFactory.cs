using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IGameStateMachine _stateMachine;
        private readonly IInstantiator _instantiator;

        public GameFactory(IAssetProvider assets, IStaticDataService staticData 
               , IGameStateMachine stateMachine, IInstantiator instantiator)
        {
            _assets = assets;
            _staticData = staticData;
            _stateMachine = stateMachine;
            _instantiator = instantiator;
        }

        public Hud CreateHud()
        {
            Hud hud = InstantiateRegister(AssetAdress.HudPath).GetComponent<Hud>();
            return hud;
        }

        private GameObject InstantiateRegister(string prefabPath, Vector3 position)
        {
            GameObject prefab = _assets.Load(prefabPath);
            GameObject gameObject = _instantiator.InstantiatePrefab(prefab,position,Quaternion.identity,null);
            return gameObject;
        }

        private GameObject InstantiateRegister(string prefabPath)
        {
            GameObject prefab = _assets.Load(prefabPath);
            GameObject gameObject = _instantiator.InstantiatePrefab(prefab);
            return gameObject;
        }
    }
}