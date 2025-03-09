using UnityEngine;
namespace Combat.Casters
{

    public class ParryCaster : MonoBehaviour, ICastable
    {
        [SerializeField] protected float _stunPower = 3f;
        public virtual void Cast(Collider2D target)
        {
            if (target.TryGetComponent(out IParryable hit))
            {
                ParryData parryData = new ParryData()
                {
                    stunPower = _stunPower
                };
                hit.Parry(parryData);
            }
        }

        public void HandleSetData(CasterData data)
        {
            // 후구현
            
        }
    }
}