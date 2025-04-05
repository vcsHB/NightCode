using Combat;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectManage.GimmickObjects.Logics
{
    public class ToggleObject : MonoBehaviour
    {
        public UnityEvent<bool> OnChangeValue;
        [SerializeField] protected LogicData _data;

        public void HandleToggle(bool isEnable)
        {
            Debug.Log(isEnable);
            OnChangeValue?.Invoke(isEnable);
        }
    }
}
