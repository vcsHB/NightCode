using Agents;
using Combat;
using UnityEngine;


namespace ObjectManage.GimmickObjects.Logics
{
    public class SpringGimmickBottom : MonoBehaviour
    {
        private bool _isGoDown = false;

        public void SetIsDown(bool isDown)
            => _isGoDown = isDown;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Vector2 direction = collision.collider.transform.position - transform.position;

            if (!_isGoDown) return;

            if (collision.collider.TryGetComponent(out IDamageable agent))
            {
                CombatData data = new CombatData();
                data.type = AttackType.Blunt;
                data.damage = 9999;
                data.damageDirection = Vector2.down;

                agent.ApplyDamage(data);
            }
        }
    }
}
