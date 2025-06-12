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
    public class EffectCaster : MonoBehaviour, ICastable
    {
        [SerializeField] private EffectStateTypeEnum _type;
        [SerializeField] private int _level;
        [SerializeField] private int _increaseStack = 1;

        public void Cast(Collider2D target)
        {
            if (target.TryGetComponent(out IEffectable effectable))
            {
                effectable.ApplyEffect(_type, _increaseStack, _level);
            }
        }

        public void HandleSetData(CasterData data)
        {
            if (data is EffectCasterData casterData)
            {
                _level = casterData.level;
                _increaseStack = casterData.increaseStack;
            }
        }
    }
}