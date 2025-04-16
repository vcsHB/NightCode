using DG.Tweening;
using UnityEngine;
namespace Agents.Enemies
{

    public class EnemyRenderer : AgentRenderer
    {
        public float  dissolveDuration;
        private Tween _dissolveTween;
        private Enemy enemy => _agent as Enemy;

        public void Dissolve()
        {
            if (_dissolveTween != null && _dissolveTween.active) return;
            _dissolveTween = _spriteRenderer.DOFade(0, dissolveDuration)
                .OnComplete(() => enemy.OnDisableBody?.Invoke(enemy, enemy.EnemyName));
        }
    }
}