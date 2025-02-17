using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace Combat
{

    public class KnockbackBody : MonoBehaviour, IKnockbackable
    {
        public UnityEvent OnCrashedEvent;
        [SerializeField] private Health _ownerHealth;
        [SerializeField] private float _knockbackResistance = 0f;
        private Rigidbody2D _rigidCompo;
        private bool _isCrashed;
        private float _crashDamage;

        private void Awake()
        {
            _rigidCompo = GetComponent<Rigidbody2D>();
        }

        public void ApplyKnockback(KnockbackCasterData knockbackData)
        {
            _rigidCompo.AddForce(knockbackData.powerDirection, ForceMode2D.Impulse);
            _isCrashed = knockbackData.isCrashed;
            StartCoroutine(KnockbackCoroutine(knockbackData.duration));
        }

        private IEnumerator KnockbackCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);

            _isCrashed = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_isCrashed)
            {
                _ownerHealth.ApplyDamage(new CombatData()
                {
                    damage = _crashDamage,
                    originPosition = other.contacts[0].point
                });
                OnCrashedEvent?.Invoke();
            }
        }
    }
}