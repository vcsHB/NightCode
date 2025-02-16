using System;
using UnityEngine;

namespace Combat
{
    public enum AttackType
    {
        Blunt = 0,
        Sharp
    }
    public struct CombatData
    {
        public AttackType type;
        public float damage;
        public Vector2 originPosition;


    }
    public interface IDamageable
    {
        public void ApplyDamage(CombatData data);
    }
}