using System;
using UnityEngine;

namespace Combat
{
    public enum AttackType
    {
        Blunt = 0,
        Sharp,
        Heat,
        Effect
    }
    [System.Serializable]
    public struct CombatData
    {
        public AttackType type;
        public float damage;
        public Vector2 originPosition;
        public Vector2 damageDirection;
        public bool invalidityResistance;


    }

    public struct HitData
    {
        public bool isHit;
        public bool isKilled; // Is Target Dead
    }
    public interface IDamageable
    {
        // 나중에 HitData를 리턴하게 해야함
        
        public bool ApplyDamage(CombatData data);
    }
}