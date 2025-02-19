using UnityEngine;

namespace Combat
{
    public interface ICastable
    {
        public void HandleSetData(CasterData data);
        public void Cast(Collider2D target);
    }
}