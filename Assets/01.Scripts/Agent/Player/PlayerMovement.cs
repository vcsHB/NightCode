using System;
using UnityEngine;
namespace Agents.Players
{

    public class PlayerMovement : MonoBehaviour, IAgentComponent
    {
        public event Action<Vector2> OnMovement;
        [Header("Ground Detect")]
        [SerializeField] private Transform _groundCheckTrm;

        [SerializeField] private Vector2 _checkerSize;
        [SerializeField] private float _checkDistance;
        [SerializeField] private LayerMask _whatIsGround;
        private Rigidbody2D _rigidCompo;

        [Header("Move Setting")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _turboPower = 30f;

        public Vector2 Velocity { get; private set; }
        private Player _player;
        private float _movementX;
        private float _moveSpeedMultiplier = 1f;
        private float _originalgravity;
        
        [field: SerializeField] public bool CanManualMove { get; set; } = true;

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _rigidCompo = agent.GetComponent<Rigidbody2D>();

            _originalgravity = _rigidCompo.gravityScale;
        }
 
        public void AfterInit() { }
        public void Dispose() { }

        private void FixedUpdate()
        {
            float xVelocity = _movementX * _moveSpeed * _moveSpeedMultiplier;
            if (CanManualMove)
            {
                if(Mathf.Abs(_movementX) > 0f) 
                    _rigidCompo.linearVelocity = new Vector2(xVelocity, _rigidCompo.linearVelocity.y);
            }
            OnMovement?.Invoke(new Vector2(xVelocity, 0));
            Velocity = _rigidCompo.linearVelocity;
        }

        public void SetMovement(float xMovement)
        {
            _movementX = xMovement;
            //_renderer.FlipController(xMovement);
        }

        public void StopImmediately(bool isYAxisToo = false)
        {
            if (isYAxisToo)
                _rigidCompo.linearVelocity = Vector2.zero;
            else
                _rigidCompo.linearVelocity = new Vector2(0, _rigidCompo.linearVelocity.y);

            _movementX = 0;
        }


        public void UseTurbo()
        {
            //float x = Mathf.Clamp(_rigidCompo.velocity.x, -1, 1) * _turboPower;
            //StopImmediately(true);
            float x = 0;
            Vector2 direction = Vector2.zero;
            if (_player.PlayerInput.InputDirection.magnitude < 0.1f)
            {
                x = _movementX * _turboPower;
                direction.y = _rigidCompo.linearVelocity.y * 0.5f;
                direction.x = x;
            }
            else
            {
                x = _player.PlayerInput.InputDirection.x * _turboPower;
                direction.x = x;
            }
            SetMultipleVelocioty(_turboPower);
            //_rigidCompo.AddForce(direction, ForceMode2D.Impulse);

        }

        // public void AddForceResetVelocity()
        // {
        //     Vector2 direction = Velocity;
        //     Debug.Log("딱 풀릴때 속력 : "+Velocity);
        //     StopImmediately(true);
        //     _rigidCompo.velocity = direction * _moveSpeedMultiplier;
        //     //AddForceToEntity(direction * _moveSpeedMultiplier);
        // }

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

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)Velocity);
        }
#endif
    }
}