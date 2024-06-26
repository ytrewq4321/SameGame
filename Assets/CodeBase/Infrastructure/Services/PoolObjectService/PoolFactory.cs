using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.PoolObjectService
{
    public class PoolFactory : IPoolFactory
    {
        private readonly IInstantiator _instantiator;

        public PoolFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public Pool Create(GameObject prefab)
        {
            Pool pool = _instantiator.Instantiate<Pool>(new[] {prefab });
            return pool;
        }
    }
}