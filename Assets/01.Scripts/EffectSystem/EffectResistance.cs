using System.Collections.Generic;
using Core.Attribute;
using UnityEditor.Tilemaps;
using UnityEngine;
namespace EffectSystem
{
    [System.Serializable]
    public class EffectData
    {
        public bool ignoreEffect;
        [ShowIf(nameof(ignoreEffect), true)] public int value;
    }

    [CreateAssetMenu(menuName = "SO/EffectSystem/EffectResistance")]
    public class EffectResistance : ScriptableObject
    {
        private Dictionary<EffectStateTypeEnum, EffectData> _resistanceValueDictionary = new();
        [SerializeField] private EffectData _fireData;
        [SerializeField] private EffectData _electricData;
        [SerializeField] private EffectData _acidData;

        private void OnEnable()
        {

            Initialize();
        }

        private void Initialize()
        {
            _resistanceValueDictionary = new Dictionary<EffectStateTypeEnum, EffectData>
            {
                { EffectStateTypeEnum.Burn, _fireData },
                { EffectStateTypeEnum.Shock, _electricData },
                { EffectStateTypeEnum.Acid, _acidData },
                { EffectStateTypeEnum.Stun, _acidData }
            };
        }

        public EffectData GetData(EffectStateTypeEnum effectType)
        {
            if (_resistanceValueDictionary.TryGetValue(effectType, out var data))
            {
                return data;
            }

            return null;
        }
    }
}