using System;
using UnityEngine;

namespace Combat
{
    public struct CombatData
    {
        public float damage;
        public Vector2 originPosition;

    }
    public interface IDamageable
    {
        public void ApplyDamage(CombatData data);
    }
}