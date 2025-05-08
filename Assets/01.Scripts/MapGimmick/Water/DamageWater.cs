using System;
using Combat.Casters;
using ObjectManage.GimmickObjects;
using UnityEngine;

namespace Ingame.Gimmick
{
    public class DamageWater : Caster, IWaterFillable, IWaterUsable
    {
        public event Action<float> OnFillEvent;
        public event Action<float> OnUseEvent;
        [SerializeField] private KnockbackCaster _knockBackCaster;
        [SerializeField] private float _knockbackPower;
        private SpriteRenderer _visualRenderer;
        public float CurrentFillLevel => _visualRenderer.size.y;
        private bool _isInited;

        protected override void Awake()
        {
            base.Awake();
            _visualRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        private void Start()
        {
        }

        public void SetFill(float fillLevel)
        {
            _visualRenderer.size = new Vector2(_visualRenderer.size.x, fillLevel);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_isInited)
            {
                _isInited = true;
                return;
            }
            if (collision.contactCount > 0)
            {
                Vector2 knockDir =
                    (Vector2)collision.collider.transform.position
                    - collision.contacts[0].point;
                knockDir.x = 0;
                knockDir.Normalize();

                _knockBackCaster.SetDirection(knockDir * _knockbackPower);
                ForceCast(collision.collider);
            }
        }

        public void Fill(float amount)
        {
            OnFillEvent?.Invoke(amount);
        }

        public void UseWater(float amount)
        {
            OnUseEvent?.Invoke(amount);
        }
    }

  
}
