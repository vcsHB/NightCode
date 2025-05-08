using System;
using Combat.Casters;
using ObjectManage.VFX;
using ObjectPooling;
using UnityEngine;
namespace Combat
{

    public class katanaSlashGenerator : MonoBehaviour
    {
        [SerializeField] private Gradient _gradientColor;
        private DamageCaster _damageCaster;
        private void Awake()
        {
            _damageCaster = GetComponent<DamageCaster>();
            _damageCaster.OnCastCombatDataEvent += HandleDamageCastEvent;
        }

        private void HandleDamageCastEvent(CombatData data)
        {
            KatanaSlashVFXPlayer vfx = PoolManager.Instance.Pop(PoolingType.KatanaSlashVFX) as KatanaSlashVFXPlayer;
            vfx.SetGradient(_gradientColor);
            vfx.Slash(transform.position, data.damageDirection);

        }
    }
}