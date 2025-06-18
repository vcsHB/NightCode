using System.Data;
using EffectSystem;
using UnityEngine;
namespace Combat.Casters
{
    public class EffectCasterData : CasterData
    {
        public EffectStateTypeEnum type;
        public int level;
        public int increaseStack;
    }

    [System.Serializable]
    public struct EffectCastingData
    {
        public EffectStateTypeEnum type;
        public int level;
        public int increaseStack;
    }
    public class EffectCaster : MonoBehaviour, ICastable
    {
        [SerializeField] private EffectCastingData[] _castingDatas;

        public void Cast(Collider2D target)
        {
            if (target.TryGetComponent(out IEffectable effectable))
            {
                for (int i = 0; i < _castingDatas.Length; i++)
                {
                    effectable.ApplyEffect(_castingDatas[i].type, _castingDatas[i].increaseStack, _castingDatas[i].level);
                }
            }
        }

        public void SetCastingData(EffectCastingData[] datas)
        {
            if (datas == null) return;
            _castingDatas = datas;
        }

        public void HandleSetData(CasterData data)
        {
        }
    }
}