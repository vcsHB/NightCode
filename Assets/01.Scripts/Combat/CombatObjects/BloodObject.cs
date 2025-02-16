using ObjectManage;
using UnityEngine;
namespace Combat.CombatObjects
{

    public class BloodObject : MonoBehaviour
    {
        [SerializeField] private int _bloodAmount = 2;
        private Health _health;
        private void Awake()
        {
            _health = GetComponent<Health>();
            _health.OnHitCombatDataEvent += HandleHitEvent;
        }

        private void HandleHitEvent(CombatData data)
        { // 실외 (배경이 벽인지) 체크 필요
            Vector2 direction = (Vector2)transform.position - data.originPosition;
            for (int i = 0; i < _bloodAmount; i++)
            {
                BloodVFXPlayer blood = PoolManager.Instance.Pop(ObjectPooling.PoolingType.BloodEffect) as BloodVFXPlayer;
                blood.transform.position = transform.position;
                blood.Play(direction);
            }
        }
    }
}