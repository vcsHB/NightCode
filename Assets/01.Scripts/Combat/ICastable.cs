using UnityEngine;

namespace Combat
{
    public interface ICastable
    {
        public void Cast(Collider2D target);
    }
}