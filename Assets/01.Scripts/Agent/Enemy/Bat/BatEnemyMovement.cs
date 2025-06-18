using System;
using UnityEngine;
namespace Agents.Enemies.Bat
{

    public class BatEnemyMovement : AgentMovement
    {
        [SerializeField] private LayerMask _ceilingLayer;
        [SerializeField] private float _moveSpeed = 5f;

        public void SetMovement(Vector2 direction)
        {
            _rigidCompo.linearVelocity = direction.normalized * _moveSpeed;
        }
        public void HandleMoveToCeiling()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 100f, _ceilingLayer);
            if(hit.collider != null)
            {
                
            }
        }
    }
}