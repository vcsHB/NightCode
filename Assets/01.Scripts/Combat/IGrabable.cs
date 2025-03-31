using UnityEngine;

namespace Combat
{
    public interface IGrabable
    {
        public Transform GetTransform { get; }
        public void OnAimEntered();
        public void OnAimExited();
        public void Grab();
        public void Release();

    }
}