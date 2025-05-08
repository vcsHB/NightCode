using UnityEngine;

namespace Base.Cafe
{
    public class CafeMaid : AvatarEntity
    {
        private CafeMaidSO _maidInfo;
        private CafeSit _targetSit;
        private bool _isDoService;

        public bool IsDoService => _isDoService;


        public void Init(CafeMaidSO maidInfo)
        {
            _maidInfo = maidInfo;
        }

        public void AssignWork(CafeSit sit)
        {
            _isDoService = true;
            _targetSit = sit;

            Transform playerPosition = sit.CalculatePlayerPosition(transform);
            SetMoveTarget(playerPosition);

            onCompleteMove += OnServe;
            stateMachine.ChangeState("Move");
        }


        private void OnServe()
        {
            stateMachine.ChangeState("Serve");
            onCompleteMove -= OnServe;
        }

        public void ServeMenu()
        {
            _targetSit.ServeByNPC();
            stateMachine.ChangeState("Talk");
        }

        public void OnCompleteTalk()
        {
            //_targetSit.ServeByPlayer
        }
    }
}
