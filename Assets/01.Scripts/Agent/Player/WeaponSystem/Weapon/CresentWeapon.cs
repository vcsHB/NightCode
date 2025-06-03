using System;
using Combat.Casters;
using UnityEngine;
using DG.Tweening;
namespace Agents.Players.WeaponSystem.Weapon
{

    public class CresentWeapon : PlayerWeapon
    {
        [SerializeField] private float _attackDuration = 1.5f;
        [SerializeField] private SpriteRenderer _attackGuideVisual;
        [SerializeField] private float _guideScaleTweenDuration = 0.15f;
        [SerializeField] private TrailRenderer _trailRenderer;
        private Caster _caster;
        private bool _isAttackEnabled;
        private DamageCasterData _damageCasterData;
        private float _currentAttackTime = 0f;

        private void Awake()
        {
            _caster = GetComponentInChildren<Caster>();
            _damageCasterData = new();
        }
        public override void Initialize(Player player)
        {
            base.Initialize(player);
            _animationTrigger.OnRopeTurboEvent.AddListener(HandleAttack);
            _animationTrigger.OnRopeRemoveEvent.AddListener(HandleRopeRemove);
        }

        private void HandleRopeRemove()
        {
            SetDisableWeapon();
        }

        public override void HandleAttack()
        {
            _isAttackEnabled = true;
            _attackGuideVisual.transform.DOScale(Vector3.one, _guideScaleTweenDuration);
            _trailRenderer.Clear();
            _trailRenderer.emitting = true;
            _currentAttackTime = 0f;
        }

        public void SetDamage(float damage)
        {
            _damageCasterData.damage = damage;
            _caster.SendCasterData(_damageCasterData);
        }

        private void Update()
        {
            if (_isAttackEnabled)
            {
                _currentAttackTime += Time.deltaTime;
                _caster.Cast();

                if (_currentAttackTime > _attackDuration)
                    SetDisableWeapon();
            }

        }

        private void SetDisableWeapon()
        {
            _isAttackEnabled = false;
            _attackGuideVisual.transform.DOScale(Vector3.zero, _guideScaleTweenDuration);
            _trailRenderer.emitting = false;
        }
    }

}