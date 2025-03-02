using UnityEngine;
namespace Agents.Enemies
{

    public abstract class EnemyAttackController : MonoBehaviour
    {
        [SerializeField] private bool _enable;

        public void SetEnable(bool value)
        {
            _enable = value;
        }

        public virtual void HandleAttack()
        {
            if (_enable)
                Attack();
        }
        
        public abstract void Attack();

    }
}