using Assets.CodeBase.Logic;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface ITileFactory : IService
    {
        public Tile CreateTile(GameObject prefab, Vector3 at, Transform parrent);
    }
}