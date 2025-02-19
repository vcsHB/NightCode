using UnityEngine;

namespace Combat
{
    public interface IGrabable
    {
        public Transform GetTransform { get; }
        public void Grab();
        public void Release();

    }
}