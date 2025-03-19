using UnityEngine;
using UnityEngine.Events;
namespace ObjectManage.GimmickObjects
{

    public abstract class SolveActor : MonoBehaviour
    {
        public UnityEvent OnSolveEvent;
        protected GimmickLogic[] _logicSolvers;
        public int LogicAmount => _logicSolvers.Length;
        private int _solveProgressLevel;
        public float SolveProgress => _solveProgressLevel / (float)LogicAmount;
        public bool IsLogicsSolved => Mathf.Approximately(SolveProgress, 1f);
        protected virtual void Awake()
        {
            Initialize();

        }


        private void Initialize()
        {

            _logicSolvers = GetComponentsInChildren<GimmickLogic>();
            foreach (GimmickLogic solver in _logicSolvers)
            {
                if (solver.DefaultSolveState)
                    _solveProgressLevel++;

                solver.OnLogicCompleteEvent.AddListener(HandleLogicSolved);
                solver.OnLogicResetEvent.AddListener(HandleLogicReset);
            }

        }

        private void HandleLogicReset()
        {
            _solveProgressLevel = 0;
        }

        private void HandleLogicSolved()
        {
            _solveProgressLevel += 1;

            if (IsLogicsSolved)
            {
                HandleSolveAct();
                OnSolveEvent?.Invoke();
            }
        }

        protected abstract void HandleSolveAct();



    }
}