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
        public Vector2 damageDirection;
        public bool invalidityResistance;


    }
    public interface IDamageable
    {
        public bool ApplyDamage(CombatData data);
    }
}