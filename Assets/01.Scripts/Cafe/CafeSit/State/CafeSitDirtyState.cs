using Base;
using UnityEngine;

namespace Base.Cafe
{
    public class CafeSitDirtyState : CafeSitState
    {
        private AvatarPlayer _player;


        public CafeSitDirtyState(CafeSit cafeSit) : base(cafeSit)
        {
        }

        public override void OnEnterState()
        {
            _cafeSit.SetInteractIcon(ECafeSitIcon.CleanIcon, true);
            _player = _cafeSit.Player;
        }

        public override void OnExitState()
        {
            _cafeSit.SetInteractIcon(ECafeSitIcon.CleanIcon, false);
            _player.RemoveClickProcessInteract(_cafeSit.CleanTable);
        }

        public override void OnTriggerEnter()
        {
            _player.AddClickProcessInteract(_cafeSit.CleanTable);
        }

        public override void OnTriggerExit()
        {
            _player.RemoveClickProcessInteract(_cafeSit.CleanTable);
        }
    }
}
