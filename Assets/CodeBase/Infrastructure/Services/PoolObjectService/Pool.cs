using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.PoolObjectService
{
    public class Pool
    {
        private readonly IInstantiator _instantiator;
        private readonly GameObject _prefab;
        private GameObject _poolRoot;
        
        private readonly Queue<GameObject> pool;

        public Pool(IInstantiator instantiator, GameObject prefab)
        {
            _instantiator = instantiator;
            _prefab = prefab;

            pool = new Queue<GameObject>();
        }

        public void Initialize()
        {
            _poolRoot = new GameObject($"{_prefab.name}_pool");
            Object.DontDestroyOnLoad(_poolRoot);
        }

        public GameObject Spawn(Transform parrent)
        {
            GameObject result = null;
            if (!pool.TryDequeue(out result))
                result = _instantiator.InstantiatePrefab(_prefab,parrent);
            result.SetActive(true);
            if(parrent!=null)
                result.transform.SetParent(parrent.transform,false);
            
            return result;
        }

        public void Despawn(GameObject gameObject)
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(_poolRoot.transform);
            pool.Enqueue(gameObject);
        }
    }
}