using Combat;
using UnityEngine;
namespace ObjectManage.GimmickObjects.Logics
{

    public class TargetAllKillLogic : GimmickLogic
    {
        [SerializeField] private Health[] _targets;
        private int _goalTargetAmount;
        private int _currentProgress;

        private void Awake()
        {
            _goalTargetAmount = _targets.Length;
            for (int i = 0; i < _goalTargetAmount; i++)
            {
                _targets[i].OnDieEvent.AddListener(HandleTargetDie);
            }
        }

        private void HandleTargetDie()
        {
            _currentProgress++;
            if (_currentProgress >= _goalTargetAmount)
            {
                Solve();
            }
        }

       
    }
}