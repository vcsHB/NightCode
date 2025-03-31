using System;
using UnityEngine;
using UnityEngine.Events;
namespace InteractSystem
{

    public class InteractTrigger : MonoBehaviour
    {
        public UnityEvent OnInteractEvent;
        public event Action<InteractData> OnInteractDataEvent;


        [Header("Interact Setting")]
        [SerializeField] private float _detectRadius;
        [SerializeField] private LayerMask _targetLayer;
        private Collider2D[] _targets; 

        public virtual void TryInteract()
        {
            Collider2D target =  Physics2D.OverlapCircle(transform.position, _detectRadius, _targetLayer);
        }

        public virtual void ForceInteract(Collider2D targetCollider)
        {
            if (targetCollider.TryGetComponent(out IInteractable interactable))
            {
                InteractData data = new InteractData()
                {
                    interactorTrm = transform,
                    interactDirection = targetCollider.transform.position - transform.position
                };
                interactable.Interact(data);
                OnInteractEvent?.Invoke();
                OnInteractDataEvent?.Invoke(data);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;

        }

#endif
    }
}