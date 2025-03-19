using UnityEngine;
namespace ObjectManage.GimmickObjects.Logics
{

    public abstract class TriggerLogic : GimmickLogic, ITriggerable
    {
        public void Trigger(LogicData data)
        {
            if (IsSolved) return;
            if (CheckSolved(data))
            {
                Solve();
            }
        }

        protected abstract bool CheckSolved(LogicData data);
    }
}