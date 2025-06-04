using System;
using UnityEngine;
namespace EffectSystem
{

    public class EffectVFXController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _effectBurstVFXs;
        [SerializeField] private ParticleSystem[] _effectLoopVFXs;
        private AgentEffectController _effectController;
        private void Awake()
        {
            _effectController = transform.parent.GetComponent<AgentEffectController>();
            _effectController.OnEffectStartEvent += StartVFX;
            _effectController.OnEffectOverEvent += StopVFX;
        }

        public void StartVFX(EffectStateTypeEnum effectType)
        {
            if (effectType == EffectStateTypeEnum.None) return;

            int index = (int)effectType - 1;
            _effectBurstVFXs[index].Play();
            _effectLoopVFXs[index].Play();
        }

        public void StopVFX(EffectStateTypeEnum effectType)
        {
            if (effectType == EffectStateTypeEnum.None) return;
            int index = (int)effectType - 1;
            _effectLoopVFXs[index].Stop();
        }

    }
}