using System.Linq;
using UnityEngine;
namespace ObjectManage.GimmickObjects
{

    public class TriggerObject : MonoBehaviour
    {
        [SerializeField] private GimmickLogic[] _triggerTargets;
        [SerializeField] private LogicData _data;
        public void HandleTrigger()
        {
            for (int i = 0; i < _triggerTargets.Length; i++)
            {
                _triggerTargets[i].Solve(_data);
            }
        }
    }
}