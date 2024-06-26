using CodeBase.Infrastructure.Services.PoolObjectService;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace Assets.CodeBase.Logic
{
    public class Tile : MonoBehaviour
    {
        private IPoolService _poolService;

        [Inject]
        private void Construct(IPoolService poolService)
        {
            _poolService = poolService;
        }

        public void Despawn()
        {
            _poolService.Despawn(this);
        }
       
        public Tween Fall(float toY,float speed)
        {
            float duration = (transform.localPosition.y - toY)/speed;
            return transform.DOMoveY(toY, duration).SetEase(Ease.InCubic);
        }

        public Tween Shift(float toX, float speed)
        {
            var duration = Mathf.Abs(transform.localPosition.x - toX) / speed;
            return transform.DOMoveX(toX, duration).SetEase(Ease.InCubic);
        }
    }
}
