using System;
using UnityEngine;
namespace Agents
{

    public class AgentMovement : MonoBehaviour, IAgentComponent
    {
        [Header("Ground Detect")]
        [SerializeField] protected Transform _groundCheckTrm;
        [SerializeField] protected Vector2 _checkerSize;
        [SerializeField] protected float _checkDistance;
        [SerializeField] protected LayerMask _whatIsGround;


        protected Rigidbody2D _rigidCompo;
        public Rigidbody2D RigidCompo => _rigidCompo;
        public Vector2 Velocity { get; protected set; }

        protected float _originalgravity;
        [field: SerializeField] public bool CanManualMove { get; set; } = true;


        protected Agent _owner;
        protected AgentRenderer _renderer;
        protected float _movementX;
        protected float _moveSpeedMultiplier = 1f;

        public virtual void Initialize(Agent agent)
        {
            _owner = agent;

            _rigidCompo = agent.GetComponent<Rigidbody2D>();
            _renderer = agent.GetCompo<AgentRenderer>();
            _originalgravity = _rigidCompo.gravityScale;
        }

        public virtual void AfterInit()
        {
        }

        public virtual void Dispose()
        {
        }
        public void SetMovement(float xMovement)
        {
            _movementX = xMovement;
            _renderer.FlipController(xMovement);
        }


        public void StopImmediately(bool isYAxisToo = false)
        {
            if (isYAxisToo)
                _rigidCompo.linearVelocity = Vector2.zero;
            else
                _rigidCompo.linearVelocity = new Vector2(0, _rigidCompo.linearVelocity.y);

            _movementX = 0;
        }
        public void SetMultipleVelocioty(float value)
        {
            _rigidCompo.linearVelocity = _rigidCompo.linearVelocity.normalized * value;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Velocity = velocity;
            _rigidCompo.linearVelocity = velocity;
        }

        public void AddForceToEntity(Vector2 power)
        {
            _rigidCompo.AddForce(power, ForceMode2D.Impulse);
        }


        public void SetMovementMultiplier(float value) => _moveSpeedMultiplier = value;
        public void SetGravityMultiplier(float value) => _rigidCompo.gravityScale = value;
        public void ResetGravityMultiplier() => _rigidCompo.gravityScale = _originalgravity;
        public virtual bool IsGroundDetected()
            => Physics2D.BoxCast(_groundCheckTrm.position, _checkerSize, 0, Vector2.down, _checkDistance, _whatIsGround);


#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            if (_groundCheckTrm != null)
            {
                Vector3 offset = new Vector3(0, _checkDistance * 0.5f);
                Gizmos.DrawWireCube(_groundCheckTrm.position - offset, new Vector3(_checkerSize.x, _checkDistance, 1f));
            }

        }

#endif
    }
}