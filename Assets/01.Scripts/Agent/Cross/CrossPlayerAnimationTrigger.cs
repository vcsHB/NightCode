using UnityEngine;
namespace Agents.Players
{

    public class CrossPlayerAnimationTrigger : PlayerAnimationTrigger
    {
        [SerializeField] private CrossPlayerAttackController _attackController;
        private bool _isReloaded;
        public void SetReload(bool value)
        {
            _isReloaded = value;
        }
        public void SwingAttack()
        {
            if (_isReloaded)
            {
                _attackController.Attack();
                SetReload(false);
            }
        }
    }
}