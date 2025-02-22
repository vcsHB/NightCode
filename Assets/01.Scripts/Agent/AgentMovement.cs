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
        protected float _movementX;
        protected float _moveSpeedMultiplier = 1f;

        public virtual void Initialize(Agent agent)
        {
            _owner = agent;
        }

        public virtual void AfterInit()
        {
        }

        public virtual void Dispose()
        {
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