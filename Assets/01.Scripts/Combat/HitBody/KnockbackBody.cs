using System.Collections;
using Combat.Casters;
using UnityEngine;
using UnityEngine.Events;
namespace Combat
{

    public class KnockbackBody : MonoBehaviour, IKnockbackable
    {
        public UnityEvent OnCrashedEvent;
        [SerializeField] protected Health _ownerHealth;
        [SerializeField] protected float _knockbackResistance = 0f;
        protected Rigidbody2D _rigidCompo;
        protected bool _isCrashed;
        protected float _crashDamage;

        protected virtual void Awake()
        {
            _rigidCompo = GetComponent<Rigidbody2D>();
        }

        public virtual void ApplyKnockback(KnockbackCasterData knockbackData)
        {
            if(_ownerHealth.IsDead) return;
            _rigidCompo.AddForce(knockbackData.powerDirection, ForceMode2D.Impulse);
            _isCrashed = knockbackData.isCrashed;
            StartCoroutine(KnockbackCoroutine(knockbackData.duration));
        }

        protected IEnumerator KnockbackCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);

            _isCrashed = false;
        }

        protected void OnCollisionEnter2D(Collision2D other)
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