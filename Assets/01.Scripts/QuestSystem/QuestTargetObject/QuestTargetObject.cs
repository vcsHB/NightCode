using System;
using Agents;
using UnityEngine;
namespace QuestSystem.QuestTarget
{

    public class QuestTargetObject : MonoBehaviour
    {
        public event Action<QeustTargetData> OnTargetCompleteEvent;
        [SerializeField] private string _targetCode;
        public string TargeCode => _targetCode;
        [SerializeField] private float _completeLevel;

        protected virtual void Complete()
        {
            OnTargetCompleteEvent?.Invoke(new QeustTargetData
            {
                targetCode = _targetCode,
                completeLevel = _completeLevel
            });
        }
    }
}