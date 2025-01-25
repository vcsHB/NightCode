using Agents.Animate;
using UnityEngine;
namespace Agents.Players
{

    public class PlayerRenderer : AgentRenderer
    {
        [field: SerializeField] public AnimParamSO IdleParam { get; private set; }
        [field: SerializeField] public AnimParamSO MoveParam { get; private set; }
        [field: SerializeField] public AnimParamSO FallParam { get; private set; }
        [field: SerializeField] public AnimParamSO JumpParam { get; private set; }
        [field: SerializeField] public AnimParamSO SwingParam { get; private set; }
        [field: SerializeField] public AnimParamSO HangParam { get; private set; }
        [field: SerializeField] public AnimParamSO EnterParam { get; private set; }
        [field: SerializeField] public AnimParamSO ExitParam { get; private set; }


        [field: SerializeField] public AnimParamSO AttackParam { get; private set; }
        [field: SerializeField] public AnimParamSO SkillParam { get; private set; }

        protected bool _isLockRotation = true;


        protected override void Awake()
        {
            base.Awake();
        }
        public void SetLockRotation(bool value)
        {
            _isLockRotation = value;
            if (value)
                transform.localRotation = Quaternion.identity;
        }

        public void SetRotate(Vector2 upDirection)
        {
            if (_isLockRotation) return;
            float offset = -90f;
            if (Mathf.Approximately(_agent.transform.eulerAngles.y, -180f))
            {
                upDirection.x *= -1;
                offset = 90f;
            }
            float angle = Mathf.Atan2(upDirection.y, upDirection.x) * Mathf.Rad2Deg + offset;
            transform.rotation = Quaternion.Euler(0, 0, angle);

        }

    }
}