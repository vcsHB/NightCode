using Agents.Animate;
using UnityEngine;
namespace Agents
{

    public class AgentRenderer : AnimateRenderer, IAgentComponent
    {

        [field: SerializeField] public float FacingDirection { get; private set; } = 1;
        protected Agent _agent;

        public void Initialize(Agent agent)
        {
            _agent = agent;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        public void AfterInit() { }
        public void Dispose() { }

        public void Flip()
        {
            FacingDirection *= -1;
            _agent.transform.Rotate(0, 180f, 0);
        }

        public void FlipController(float normalizeXMove)
        {
            if (Mathf.Abs(FacingDirection + normalizeXMove) < 0.5f)
                Flip();
        }

    }
}