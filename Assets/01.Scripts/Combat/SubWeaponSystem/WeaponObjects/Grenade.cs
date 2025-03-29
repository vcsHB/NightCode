using System;
using System.Collections;
using Combat.Casters;
using UnityEngine;
using UnityEngine.Events;

namespace Combat.SubWeaponSystem
{

    public abstract class Grenade : ThrowingWepaonObject
    {
        public UnityEvent OnExplosionEvent;
        [SerializeField] protected Caster _caster;
        [SerializeField] protected float _disableDelay;
        public event Action<float, float> OnDelayTimeEvent;
        [SerializeField] protected float _explosionDelay = 4f;
        protected SpriteRenderer _visualRenderer;

        protected override void Awake()
        {
            _visualRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
        }

        public override void ResetObject()
        {
            base.ResetObject();
            _visualRenderer.enabled = true;
        }

        public override void UseWeapon(SubWeaponControlData data)
        {
            base.UseWeapon(data);
            StartCoroutine(ExplosionDelayCoroutine());
            _rigid.angularVelocity = data.direction.x * 4f;
        }

        protected virtual IEnumerator ExplosionDelayCoroutine()
        {
            float currentTime = 0f;
            while (currentTime < _explosionDelay)
            {
                currentTime += Time.deltaTime;
                OnDelayTimeEvent?.Invoke(currentTime, _explosionDelay);
                yield return null;
            }
            OnDelayTimeEvent?.Invoke(_explosionDelay, _explosionDelay);
            Explode();
            _visualRenderer.enabled = false;
        }


        public abstract void Explode();
    }
}