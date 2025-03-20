using UnityEngine;
using DG.Tweening;
using Core;
namespace Agents.Players
{

    public class CresentPlayerSwingTrajectoryVisual : MonoBehaviour, IAgentComponent
    {

        [SerializeField] private Transform _visualTrm;
        [SerializeField] private Rigidbody2D _rigid;
        private SpriteRenderer _spriteRenderer;
        private bool _isActive;
        private CresentPlayer _owner;

        private void Awake()
        {
            _spriteRenderer = _visualTrm.GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            if (_isActive)
            {
                Vector2 velocity = _rigid.linearVelocity;
                if (velocity.magnitude > _owner.DashAttackStandardVelocity)
                {
                    _visualTrm.DOScaleY(1f, 0.1f);
                    //_spriteRenderer.enabled = true;
                    SetDirection(velocity.normalized);
                    return;
                }
                _visualTrm.DOScaleY(0f, 0.1f);
                //_spriteRenderer.enabled = false;

            }

        }

        public void SetVisualEnable(bool value)
        {
            _isActive = value;
            if (!value)
                _visualTrm.DOScaleY(0f, 0.1f);

 //               _spriteRenderer.enabled = false;
        }
        public void SetDirection(Vector2 direction)
        {
            Vector2 clampedDirection = VectorCalculator.ClampTo8Directions(direction);
            _visualTrm.right = clampedDirection;

        }

        public void Initialize(Agent agent)
        {
            _owner = agent as CresentPlayer;
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }
    }
}