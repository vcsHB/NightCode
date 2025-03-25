using ObjectManage.GimmickObjects.Logics;
using UnityEngine;
using UnityEngine.Events;
namespace ObjectManage.GimmickObjects
{

    public class TriggerObject : MonoBehaviour
    {
        public UnityEvent OnTriggerEvent;
        [SerializeField] protected TriggerLogic[] _triggerTargets;
        [SerializeField] protected LogicData _data;

        [ContextMenu("DebugTrigger")]
        public void HandleTrigger()
        {
            for (int i = 0; i < _triggerTargets.Length; i++)
            {
                _triggerTargets[i].Trigger(_data);
            }
            OnTriggerEvent?.Invoke();
        }
    }
}