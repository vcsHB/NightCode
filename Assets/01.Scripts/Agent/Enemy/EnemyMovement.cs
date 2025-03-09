using UnityEngine;
namespace Agents.Enemies
{

    public class EnemyMovement : AgentMovement
    {
        [SerializeField] private float _moveSpeed = 4f;

        
        private void FixedUpdate()
        {
            float xVelocity = _movementX * _moveSpeed * _moveSpeedMultiplier;
            if (CanManualMove)
            {
                if (Mathf.Abs(_movementX) > 0f)
                    _rigidCompo.linearVelocity = new Vector2(xVelocity, _rigidCompo.linearVelocity.y);
            }
            Velocity = _rigidCompo.linearVelocity;
        }

    }
}