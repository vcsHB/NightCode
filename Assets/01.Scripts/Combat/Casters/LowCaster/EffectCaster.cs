using EffectSystem;
using UnityEngine;
namespace Combat.Casters
{
    public class EffectCasterData : CasterData
    {
        public EffectStateTypeEnum type;
        public int level;
        public float duration;
    }
    public class EffectCaster : MonoBehaviour, ICastable
    {
        [SerializeField] private EffectStateTypeEnum _type;
        [SerializeField] private int _level;
        [SerializeField] private float _duration;

        public void Cast(Collider2D target)
        {
            if (target.TryGetComponent(out IEffectable effectable))
            {
                effectable.ApplyEffect(_type, _duration, _level);
            }
        }

        public void HandleSetData(CasterData data)
        {
            if (data is EffectCasterData casterData)
            {

            }
        }
    }
}