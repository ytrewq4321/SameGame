using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _bootstrapperPrefab;
        private IInstantiator _instantiator;
        
        [Inject]
        public void Constructor(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        private void Awake()
        {
            var bootstapper = FindFirstObjectByType<GameBootstrapper>();

            if (bootstapper == null)
            {
                _instantiator.InstantiatePrefab(_bootstrapperPrefab);
            }
        }
    }
}