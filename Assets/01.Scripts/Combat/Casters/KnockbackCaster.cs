using UnityEngine;
namespace Combat
{
    [System.Serializable]
    public class KnockbackCasterData : CasterData
    {
        public Vector2 powerDirection;
        public float duration;
        public bool isCrashed; // After Collision Damage By Wall or Floor
        public float crashDamage;
    }
    public class KnockbackCaster : MonoBehaviour, ICastable
    {
        [SerializeField] private KnockbackCasterData _knockbackData;

        public void Cast(Collider2D target)
        {
            if (_knockbackData == null) return;

            if (target.TryGetComponent(out IKnockbackable knockbackable))
            {
                knockbackable.ApplyKnockback(_knockbackData);
            }
        }

        public void HandleSetData(CasterData data)
        {
            _knockbackData = data as KnockbackCasterData;
            if (_knockbackData == null) return;

        }
    }
}