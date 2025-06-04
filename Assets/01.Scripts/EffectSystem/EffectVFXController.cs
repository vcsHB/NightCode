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
            _effectController = GetComponent<AgentEffectController>();
            _effectController.OnEffectStartEvent += StartVFX;
            _effectController.OnEffectStartEvent += StopVFX;
        }

        public void StartVFX(EffectStateTypeEnum effectType)
        {
            int index = GetIndexFromFlag(effectType);
            _effectBurstVFXs[index].Play();
            _effectLoopVFXs[index].Play();
        }

        public void StopVFX(EffectStateTypeEnum effectType)
        {
            int index = GetIndexFromFlag(effectType);
            _effectLoopVFXs[index].Stop();
        }

        int GetIndexFromFlag(EffectStateTypeEnum effect)
        {
            if (effect == EffectStateTypeEnum.None)
                throw new ArgumentException("Effect cannot be None");

            int value = (int)effect;

            if ((value & (value - 1)) != 0)
                throw new ArgumentException("Multiple flags are set; only single flags are allowed.");

            return (int)Mathf.Log(value);
        }

        private EffectStateTypeEnum GetEffectByIndex(int index)
        {
            return (EffectStateTypeEnum)(1 << index);
        }
    }
}