using Combat;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players
{
    public class GrabController : MonoBehaviour, IAgentComponent
    {
        public UnityEvent OnPullEvent;
        public UnityEvent OnThrowEvent;

        private Player _player;

        public bool IsPulled { get; private set; }
        private GrabData _currentGrabData;
        private AimData _currentAimData;

        private AgentRenderer _agentRenderer;
        private Transform _grabTargetTrm;
        private IGrabable _grabTarget;


        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _agentRenderer = _player.GetCompo<AgentRenderer>();
            AimDetector aimDetector = _player.GetCompo<AimDetector>();

            aimDetector.OnGrabEvent += HandleRefreshGrab;
            _player.PlayerInput.OnShootEvent += HandleShootEvent;
        }

        private void HandleShootEvent(bool value)
        {
            if (_player.IsActive)
            {
                if (value)
                {
                    if (_currentGrabData.isTargeted)
                    {
                        _grabTarget = _currentGrabData.grabTarget;
                        _grabTarget.Grab();
                    }
                }
                else
                {
                    if (_grabTarget != null)
                    {
                        _grabTarget.Release();
                        _grabTarget = null;

                    }
                }
            }
        }


        private void HandleRefreshGrab(GrabData data)
        {
            _currentGrabData = data;    
        }

        public void AfterInit() { }
        public void Dispose()
        {
            AimDetector aimDetector = _player.GetCompo<AimDetector>();
            aimDetector.OnGrabEvent -= HandleRefreshGrab;
            _player.PlayerInput.OnShootEvent -= HandleShootEvent;
        }


    }
}