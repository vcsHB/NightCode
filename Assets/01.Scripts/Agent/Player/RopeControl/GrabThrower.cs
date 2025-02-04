using System;
using UnityEngine;
namespace Agents.Players
{
    public class GrabThrower : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private ThrowDirectionMark _throwDirectionMark;
        private bool _isComboComplete;
        private Player _player;
        
        [SerializeField] private float _throwPower;


        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            AimDetector aimDetector = _player.GetCompo<AimDetector>();
            aimDetector.OnAimEvent += HandleRefreshAim;
            aimDetector.OnGrabEvent += HandleRefreshGrab;
        }

        public void SetCompleteCombo()
        {
            _isComboComplete = true;
            _throwDirectionMark.SetTargetMark(true);
        }

        public void ThrowTarget()
        {
            if (_isComboComplete)
            {
                // 에임 방향으로 던지기
            }
            else
            {
                // 포물선 위로 던지기
            }
            _isComboComplete = false;
            _throwDirectionMark.SetTargetMark(false);
        }






        private void HandleRefreshAim(AimData data)
        {
            _throwDirectionMark.SetDirection(data.aimDirection);
        }

        private void HandleRefreshGrab(GrabData data)
        {

        }
    }


}