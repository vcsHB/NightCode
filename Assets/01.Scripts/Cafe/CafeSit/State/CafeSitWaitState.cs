using UnityEngine;

namespace Cafe
{
    public class CafeSitWaitState : CafeSitState
    {
        public CafeSitWaitState(CafeSit cafeSit) : base(cafeSit)
        {
        }

        public override void OnEnterState()
        {
            _cafeSit.SetInteractIcon(ECafeSitIcon.FoodIcon, true);
        }

        public override void OnExitState()
        {
            _cafeSit.SetInteractIcon(ECafeSitIcon.FoodIcon, false);

            if (_cafeSit.IsInteractive)
                _cafeSit.Player.RemoveInteract(_cafeSit.ServeByPlayer);
        }

        public override void OnTriggerEnter()
        {
            if (_cafeSit.IsInteractive && _cafeSit.Player.isGetFood && _cafeSit.IsGetFood == false)
                _cafeSit.Player.AddInteract(_cafeSit.ServeByPlayer);
        }

        public override void OnTriggerExit()
        {
            if (_cafeSit.IsInteractive)
                _cafeSit.Player.AddInteract(_cafeSit.ServeByPlayer);
        }
    }
}
