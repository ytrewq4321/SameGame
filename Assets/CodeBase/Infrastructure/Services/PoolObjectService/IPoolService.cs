using UnityEngine;

namespace CodeBase.Infrastructure.Services.PoolObjectService
{
    public interface IPoolService : IService
    {
        public GameObject Spawn(GameObject prefab, Transform parrent = null);
        public void Despawn(GameObject gameObject);
        public TComponent Spawn<TComponent>(GameObject prefab, Transform parrent = null) where TComponent : MonoBehaviour;
        public void Despawn<TComponent>(TComponent gameObject) where TComponent : MonoBehaviour;
    }
}