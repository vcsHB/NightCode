using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies.Drones
{

    public abstract class DroneWeapon : MonoBehaviour
    {
        public UnityEvent OnAttackEvent;
        public void HandleAttack()
        {
            OnAttackEvent?.Invoke();
        }

        public abstract void Attack();
    }
}