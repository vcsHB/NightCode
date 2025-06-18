using ObjectManage.VFX;
using ObjectPooling;
using UnityEngine;
namespace Agents.Players.WeaponSystem.Weapon.WeaponObjects
{

    public class ThrowingDagger : WeaponPoolingProjectileObject
    {
        [SerializeField] private Gradient _slashGradient;
        private SpriteRenderer _visualRenderer;


        protected override void Awake()
        {
            base.Awake();
            _visualRenderer = _visualTrm.GetComponent<SpriteRenderer>();
            _mainCaster.OnCastSuccessEvent.AddListener(HandleProjectileDestroy);

        }

        private void FixedUpdate()
        {
            _mainCaster.Cast();
        }

        private void HandleProjectileDestroy()
        {
            KatanaSlashVFXPlayer vfx = PoolManager.Instance.Pop(PoolingType.KatanaSlashVFX) as KatanaSlashVFXPlayer;
            vfx.Slash(transform.position, _direction, 60f);
            vfx.SetGradient(_slashGradient);
            ReturnToPool();
        }
    }
}