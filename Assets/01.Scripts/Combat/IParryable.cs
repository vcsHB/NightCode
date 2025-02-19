using System.Reflection;

namespace Combat
{
    public struct ParryData
    {
        public float stunPower;
    }
    public interface IParryable
    {
        public bool Parry(ParryData data);

        public void SetCanParry(bool value);
    }
}