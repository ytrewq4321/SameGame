using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.PoolObjectService
{
    public class PoolService : IPoolService
    {
        private readonly IPoolFactory _poolFactory;
        
        private Dictionary<int, Pool> _prefabsPool;
        private Dictionary<int, Pool> _spawnedObjectsPool;

        public PoolService(IPoolFactory poolFactory)
        {
            _poolFactory = poolFactory;

            _prefabsPool = new Dictionary<int, Pool>();
            _spawnedObjectsPool = new Dictionary<int, Pool>();
        }

        public GameObject Spawn(GameObject prefab, Transform parent = null)
        {
            Pool pool = null;
            int prefabInstanceId = prefab.GetInstanceID();

            if (!_prefabsPool.TryGetValue(prefabInstanceId, out pool))
            {
                pool = CreatePool(prefab);
            }

            GameObject spawnedObject = pool.Spawn(parent);
            _spawnedObjectsPool.Add(spawnedObject.GetInstanceID(),pool);

            return spawnedObject;
        }
        
        public void Despawn(GameObject gameObject)
        {
            int prefabIntanceId = gameObject.GetInstanceID();
            if(_spawnedObjectsPool.TryGetValue(prefabIntanceId,out Pool pool))
            {
                _spawnedObjectsPool.Remove(prefabIntanceId);
                pool.Despawn(gameObject);
            }
        }

        public TComponent Spawn<TComponent>(GameObject prefab, Transform parrent = null) where TComponent : MonoBehaviour
        {
            GameObject spawnedObject = Spawn(prefab.gameObject,parrent);
            return spawnedObject.GetComponent<TComponent>();
        }

        public void Despawn<TComponent>(TComponent gameObject) where TComponent : MonoBehaviour
        {
            Despawn(gameObject.gameObject);
        }

        private Pool CreatePool(GameObject prefab)
        {
            Pool pool = _poolFactory.Create(prefab);
            pool.Initialize();
            _prefabsPool.Add(prefab.GetInstanceID(),pool);
            return pool;
        }
    }
}