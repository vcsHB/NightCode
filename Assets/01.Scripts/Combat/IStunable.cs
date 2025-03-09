using UnityEngine;
namespace Combat
{
    public enum StunType
    {
        Grabbed,
        Groggy,
        Shock,
        Fall,
        Crashed
    }

    public struct StunData
    {
        public StunType stunType;
        public float power;
        public float duration;
    }
    public interface IStunable
    {
        public bool Stun(StunData stunData);
        
    }
}