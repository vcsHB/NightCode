
using UnityEngine;

namespace Core
{
    public static class VectorCalculator
    {
        public static readonly Vector2[] directions = {
            Vector2.up,            // (0, 1)  위
            Vector2.down,          // (0, -1) 아래
            Vector2.left,          // (-1, 0) 왼쪽
            Vector2.right,         // (1, 0)  오른쪽
            new Vector2(1, 1).normalized,   // ↗
            new Vector2(-1, 1).normalized,  // ↖
            new Vector2(1, -1).normalized,  // ↘
            new Vector2(-1, -1).normalized  // ↙
        };

        public static Vector2 ClampTo8Directions(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return Vector2.zero;

            direction.Normalize();

            // 가장 가까운 방향 찾기
            float maxDot = -Mathf.Infinity;
            Vector2 bestDirection = Vector2.zero;

            foreach (Vector2 dir in directions)
            {
                float dot = Vector2.Dot(direction, dir);
                if (dot > maxDot)
                {
                    maxDot = dot;
                    bestDirection = dir;
                }
            }

            return bestDirection;
        }

    }
}