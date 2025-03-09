using UnityEngine;

namespace Combat.Casters
{
    public interface ICastable
    {
        public void HandleSetData(CasterData data);
        public void Cast(Collider2D target);
    }
}