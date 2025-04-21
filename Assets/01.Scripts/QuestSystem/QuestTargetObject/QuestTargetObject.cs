using System;
using UnityEngine;
namespace QuestSystem.QuestTarget
{

    public class QuestTargetObject : MonoBehaviour
    {
        public event Action<QuestTargetData> OnTargetCompleteEvent;
        [SerializeField] private string _targetCode;
        public string TargeCode => _targetCode;
        [SerializeField] private float _completeLevel;

        protected virtual void Complete()
        {
            OnTargetCompleteEvent?.Invoke(new QuestTargetData
            {
                targetCode = _targetCode,
                completeLevel = _completeLevel
            });
        }
    }
}