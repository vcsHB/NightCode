using UnityEngine;
namespace Combat.Casters
{

    public class DirectionCaster : Caster
    {
        protected Vector2 _direction;
        [SerializeField] protected float _detectDistance = 3f;

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }


    }
}