using UnityEngine;

namespace Base.Cafe
{
    public abstract class CafeSitState
    {
        protected CafeSit _cafeSit;

        public CafeSitState(CafeSit cafeSit)
        {
            _cafeSit = cafeSit;
        }

        public abstract void OnEnterState();
        public abstract void OnExitState();

        public abstract void OnTriggerEnter();
        public abstract void OnTriggerExit();
    }
}
