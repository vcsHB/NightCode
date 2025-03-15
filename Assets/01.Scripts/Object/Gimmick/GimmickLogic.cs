using UnityEngine;
using UnityEngine.Events;

namespace ObjectManage.GimmickObjects
{

    public enum LogicType
    {
        Add,
        Or
    }


    public class GimmickLogic : MonoBehaviour, ISovleable
    {
        public UnityEvent OnLogicCompleteEvent;
        public UnityEvent OnLogicResetEvent;
        public LogicType logicType;

        [SerializeField] private bool _defaultSolveState;
        public bool DefaultSolveState => _defaultSolveState;
        public bool IsSolved { get; set; }

        public void ResetGimmick()
        {
            if(IsSolved == _defaultSolveState) return;
            IsSolved = _defaultSolveState;
            OnLogicResetEvent?.Invoke();
        }

        public virtual void Solve(LogicData data)
        {
            if(IsSolved) return;
            OnLogicCompleteEvent?.Invoke();
            IsSolved = true;
        }

        protected virtual void CheckSolved()
        {

        }


    }
}