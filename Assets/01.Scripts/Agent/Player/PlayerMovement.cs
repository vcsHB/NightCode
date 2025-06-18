using System;
using UnityEngine;
namespace Agents.Players
{

    public class PlayerMovement : AgentMovement, IAgentComponent
    {
        public event Action<Vector2> OnMovement;

        [Header("Wall Detect")]
        [SerializeField] private Transform _wallCheckerTrm;
        [SerializeField] private float _wallDetectDistance;
        public float WallDirection { get; private set; }

        [Header("Move Setting")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _turboPower = 30f;
        [SerializeField] private float _velocityLimit = 50f;

        private Player _player;
        private float _movementY;

        private int _maxTurboCount = 1;
        private int _currentTurboCount = 0;
        public bool CanUseTurbo => _currentTurboCount < _maxTurboCount;
        public int jumpCount = 1;
        public bool CanJump => jumpCount > 0;
        private PlayerRenderer _playerRenderer;

        public override void Initialize(Agent agent)
        {
            base.Initialize(agent);
            _player = agent as Player;
            _playerRenderer = agent.GetCompo<PlayerRenderer>();

        }


        private void FixedUpdate()
        {
            float xVelocity = _movementX * _speedStat.Value * _moveSpeedMultiplier;
            if (CanManualMove)
            {
                if (Mathf.Abs(_movementY) > 0f)
                    _rigidCompo.linearVelocity = new Vector2(0f, _movementY);
                else if (Mathf.Abs(_movementX) > 0f)
                    _rigidCompo.linearVelocity = new Vector2(xVelocity, _rigidCompo.linearVelocity.y);
            }
            ClampVelocity();
            Velocity = _rigidCompo.linearVelocity;
            OnMovement?.Invoke(Velocity);
        }

        public void ClampVelocity()
        {
            _rigidCompo.linearVelocity = Vector2.ClampMagnitude(_rigidCompo.linearVelocity, _velocityLimit);
        }

        public void ClampVelocityWithMoveSpeed()
        {
            _rigidCompo.linearVelocity = Vector2.ClampMagnitude(_rigidCompo.linearVelocity, _speedStat.Value * _moveSpeedMultiplier);
        }

        public void SetYMovement(float yMovement)
        {
            _movementY = yMovement;
        }

        public void StopYVelocity()
        {
            _rigidCompo.linearVelocityY = 0f;
        }


        public void UseTurbo(Vector2 hangingDirection)
        {
            hangingDirection.Normalize();
            Debug.DrawLine(_player.transform.position, _player.transform.position + (Vector3)hangingDirection * 10, Color.magenta, 2f);
            _currentTurboCount++;
            Vector2 rotatedDirection = new Vector2(-hangingDirection.y, hangingDirection.x);
            rotatedDirection.Normalize();

            Vector2 inputDirection = _player.PlayerInput.InputDirection;
            if (inputDirection.magnitude < 0.1f)
                inputDirection = Velocity.normalized;
            inputDirection.y = 0f;

            Vector2 result = rotatedDirection * Vector2.Dot(inputDirection, rotatedDirection);
            result.Normalize();
            //Debug.DrawLine(_player.transform.position, _player.transform.position + (Vector3)result, Color.magenta, 2f);

            SetVelocity(result * _turboPower);
        }

        public void ResetTurboCount()
        {
            _currentTurboCount = 0;
        }

        public void AddTurboCount(int amount)
        {
            _maxTurboCount += amount;
        }



        public virtual bool IsWallDetected()
        {
            if (IsDirectionWall(Vector2.left))
                return true;
            if (IsDirectionWall(Vector2.right))
                return true;
            WallDirection = 0f;
            return false;
        }
        private bool IsDirectionWall(Vector2 direction)
        {
            bool isWall = Physics2D.Raycast(_wallCheckerTrm.position, direction, _wallDetectDistance, _whatIsGround);
            if (isWall)
                WallDirection = direction.x;
            return isWall;
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)Velocity);
            if (_wallCheckerTrm != null)
            {
                Gizmos.DrawLine(_wallCheckerTrm.position, _wallCheckerTrm.position + (Vector3)(Vector2.left * _wallDetectDistance));
                Gizmos.DrawLine(_wallCheckerTrm.position, _wallCheckerTrm.position + (Vector3)(Vector2.right * _wallDetectDistance));
            }
        }
#endif
    }
}