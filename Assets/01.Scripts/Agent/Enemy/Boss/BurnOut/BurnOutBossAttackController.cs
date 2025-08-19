using UnityEngine;
namespace Agents.Enemies.BossManage
{
    public class BurnOutBossAttackController : EnemyAttackController
    {
        [SerializeField] private BurnOutLaser _laser;
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private float _wallDetectDistance = 30f;

        public Vector2 GetWallDirection()
        {
            Vector2 position = transform.position;

            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.left, _wallDetectDistance, _wallLayer);
            if (hit)
                return (hit.point - position).normalized;

            hit = Physics2D.Raycast(position, Vector2.right, _wallDetectDistance, _wallLayer);
            if (hit)
                return (hit.point - position).normalized;

            return Vector2.zero;
        }

        public override void Attack()
        {
        }

        public void SetLaserActive(bool value)
        {
            if (value)
                _laser.StartFire();
            else
                _laser.StopFire();
        }

        public override void Initialize(Agent agent)
        {
            
        }
    }
}