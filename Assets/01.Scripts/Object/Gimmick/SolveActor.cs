using System;
using UnityEngine;
namespace ObjectManage.GimmickObjects
{

    public class SolveActor : MonoBehaviour
    {
        private GimmickLogic[] _logicSolvers;
        public int LogicAmount => _logicSolvers.Length;
        private int _solveProgressLevel;
        public float SolveProgress => _solveProgressLevel / (float)LogicAmount;
        private void Awake()
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
        }

        private void HandleLogicSolved()
        {
            _solveProgressLevel += 1;
        }
    }
}