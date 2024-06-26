using Assets.CodeBase.Logic;
using CodeBase.Infrastructure.Services.PoolObjectService;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class TileFactory : ITileFactory
    {
        private readonly IPoolService _poolService;

        public TileFactory(IPoolService poolService)
        {
            _poolService = poolService;
        }

        public Tile CreateTile(GameObject prefab, Vector3 at)
        {
            var tile =_poolService.Spawn<Tile>(prefab); 
            tile.transform.position = at;
            return tile;         
        }
    }
}
