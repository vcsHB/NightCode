using System;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Events;
namespace InteractSystem
{

    public class InteractTrigger : MonoBehaviour
    {
        public UnityEvent OnInteractEnterEvent;
        public UnityEvent OnInteractExitEvent;
        public UnityEvent OnInteractEvent;
        public event Action<InteractData> OnInteractDataEvent;


        [Header("Interact Setting")]
        [SerializeField] private float _detectRadius;
        [SerializeField] private LayerMask _targetLayer;
        private Collider2D _currentTarget;
        protected IInteractable _interactTarget;

        public virtual void DetectTarget()
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, _detectRadius, _targetLayer);
            if (target == null)
            {
                if (_currentTarget != null)
                {
                    _interactTarget.DetectExit();
                    OnInteractExitEvent?.Invoke();
                }
                _currentTarget = null;
                _interactTarget = null;
                return;
            }
            if (_currentTarget == target) return;
            if (target.TryGetComponent(out IInteractable interactTarget))
            {
                _currentTarget = target;
                _interactTarget = interactTarget;

                _interactTarget.DetectEnter();
                OnInteractEnterEvent?.Invoke();
            }
        }

        public void TryInteract()
        {
            if (_interactTarget == null) return;
            InteractData data = new InteractData()
            {
                interactorTrm = transform,
                interactDirection = _currentTarget.transform.position - transform.position
            };
            _interactTarget.Interact(data);
            OnInteractEvent?.Invoke();
            OnInteractDataEvent?.Invoke(data);
        }



#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _detectRadius);
        }

#endif
    }
}