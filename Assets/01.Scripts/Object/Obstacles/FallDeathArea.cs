using Combat.PlayerTagSystem;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectManage
{

    public class FallDeathArea : MonoBehaviour
    {
        public UnityEvent OnEnterEvent;
        [SerializeField] private float _damage;
        [SerializeField] private Transform _backPoint;


        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerManager.Instance.SetCurrentPlayerPosition(_backPoint.position);

            OnEnterEvent?.Invoke();
            
        }

    }

}