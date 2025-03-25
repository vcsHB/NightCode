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
        private AimController _aimController;
        

        private void Awake()
        {
            _spriteRenderer = _visualTrm.GetComponent<SpriteRenderer>();
            _aimController = _owner.GetCompo<AimController>();
        }

        private void FixedUpdate()
        {
            if (_isActive)
            {
                Vector2 velocity = _rigid.linearVelocity;
                if (velocity.magnitude > _owner.DashAttackStandardVelocity)
                {
                    Time.timeScale = 0.7f;

                    _visualTrm.DOScaleY(1f, 0.1f);
                    //_spriteRenderer.enabled = true;
                    SetDirection(_aimController.AimDirection);
                    return;
                }
                Time.timeScale = 1f;
                _visualTrm.DOScaleY(0f, 0.1f);
                //_spriteRenderer.enabled = false;

            }

        }

        public void SetVisualEnable(bool value)
        {
            _isActive = value;
            if (!value)
                _visualTrm.DOScaleY(0f, 0.1f);
        }
        public void SetDirection(Vector2 direction)
        {
            //Vector2 clampedDirection = VectorCalculator.ClampTo8Directions(direction);
            _visualTrm.right = direction;

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