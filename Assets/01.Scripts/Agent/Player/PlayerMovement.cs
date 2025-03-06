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
            float xVelocity = _movementX * _moveSpeed * _moveSpeedMultiplier;
            if (CanManualMove)
            {
                if (Mathf.Abs(_movementY) > 0f)
                    _rigidCompo.linearVelocity = new Vector2(0f, _movementY);
                else if (Mathf.Abs(_movementX) > 0f)
                    _rigidCompo.linearVelocity = new Vector2(xVelocity, _rigidCompo.linearVelocity.y);
            }
            OnMovement?.Invoke(new Vector2(xVelocity, 0));
            ClampVelocity();
            Velocity = _rigidCompo.linearVelocity;
        }

        private void ClampVelocity()
        {
            _rigidCompo.linearVelocity = Vector2.ClampMagnitude(_rigidCompo.linearVelocity, _velocityLimit);
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
            Vector2 baseDirection = -hangingDirection.normalized;
            Vector2 inputDirection = new Vector2(_player.PlayerInput.InputDirection.x, 0f);
            if (inputDirection.magnitude < 0.1f)
                inputDirection = Velocity.normalized;

            // 1. 입력 벡터를 HangingDirection 벡터에 투영
            Vector2 projection = Vector3.Project(inputDirection, baseDirection);
            projection.Normalize();

            // 2. 입력 벡터를 HangingDirection 벡터에 수직인 방향으로 분리
            Vector2 perpendicular = inputDirection - projection;

            // 3. 결과 벡터 계산 (필요한 연산 방식에 따라 다르게 적용 가능)
            Vector2 result = baseDirection + projection.normalized + perpendicular.normalized;

            SetVelocity(result.normalized * _turboPower);
        }

        

        public virtual bool IsWallDetected()
        {
            if(IsDirectionWall(Vector2.left))
                return true;
            if(IsDirectionWall(Vector2.right))
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
            if(_wallCheckerTrm != null)
            {
                Gizmos.DrawLine(_wallCheckerTrm.position, _wallCheckerTrm.position + (Vector3)(Vector2.left * _wallDetectDistance));
                Gizmos.DrawLine(_wallCheckerTrm.position, _wallCheckerTrm.position + (Vector3)(Vector2.right * _wallDetectDistance));
            }
        }
#endif
    }
}