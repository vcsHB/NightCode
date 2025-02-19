using UnityEngine;

namespace Combat.CombatObjects.ProjectileManage
{
    [System.Serializable]
    public struct ProjectileData
    {
        public Vector2 direction;
        public float speed;
        public float lifeTime;
        public bool canPenetrate; // 관통력
        public bool canDestroy;
        public float damage;
    }
}