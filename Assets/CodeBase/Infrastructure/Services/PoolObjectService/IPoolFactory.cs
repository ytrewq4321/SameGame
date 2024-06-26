using UnityEngine;

namespace CodeBase.Infrastructure.Services.PoolObjectService
{
    public interface IPoolFactory
    {
        public Pool Create(GameObject prefab);
    }
}