using System;
using Combat;
using UnityEngine;
using UnityEngine.Events;
namespace ObjectManage.Obstacles
{

    public class Obstacle : MonoBehaviour
    {
        public UnityEvent OnDestroyEvent;
        [SerializeField] protected ParticleSystem _hitVFX;
        [SerializeField] protected ParticleSystem _destroyVFX;

        protected Transform _visualTrm;
        protected SpriteRenderer _visualRenderer;
        protected Health _healthCompo;
        protected Collider2D _collider;


        protected virtual void Awake()
        {
            _visualTrm = transform.Find("Visual");
            _visualRenderer = _visualTrm.GetComponent<SpriteRenderer>();
            _healthCompo = GetComponent<Health>();
            _healthCompo.OnHealthChangedEvent.AddListener(HandleHitEvent);
            _healthCompo.OnDieEvent.AddListener(HandleDieEvent);
        }

        protected void HandleHitEvent()
        {
            _hitVFX.Play();
        }

        protected void HandleDieEvent()
        {
            _destroyVFX.Play();
            _collider.enabled = false;
            _visualRenderer.enabled = false;
        }
    }
}