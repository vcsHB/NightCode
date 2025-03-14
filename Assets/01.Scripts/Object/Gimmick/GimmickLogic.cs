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
        public LogicType logicType;

        public virtual void Solve()
        {

        }

        protected virtual void CheckSolved()
        {
            
        }


    }
}