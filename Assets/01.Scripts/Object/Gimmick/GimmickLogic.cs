using UnityEngine;
using UnityEngine.Events;

namespace ObjectManage.GimmickObjects
{

    public enum LogicType
    {
        Add,
        Or
    }


    public abstract class GimmickLogic : MonoBehaviour, ISovleable
    {
        public UnityEvent OnLogicCompleteEvent;
        public UnityEvent OnLogicResetEvent;
        public LogicType logicType;

        [SerializeField] private bool _defaultSolveState;
        public bool DefaultSolveState => _defaultSolveState;
        public bool IsSolved { get; set; }

        public void ResetGimmick()
        {
            if (IsSolved == _defaultSolveState) return;
            IsSolved = _defaultSolveState;
            OnLogicResetEvent?.Invoke();
        }

        public virtual void Solve()
        {
            if (IsSolved) return;
            IsSolved = true;
            OnLogicCompleteEvent?.Invoke();
        }




    }
}