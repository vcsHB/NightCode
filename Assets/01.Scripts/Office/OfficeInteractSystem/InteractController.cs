using InputManage;
using UnityEngine;

namespace Office.InteractSystem
{
    public class InteractController : MonoBehaviour
    {
        [SerializeField] private UIInputReader _uiInputReader;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _detectRadius = 1f;
        private bool _canInteract = true;
        private IInteractable _interactTarget;
        private Vector2 _detectCenterPos;

        private void Awake()
        {
            _uiInputReader.OnLeftClickEvent += Interact;
        }

        private void OnDestroy()
        {
            _uiInputReader.OnLeftClickEvent -= Interact;

        }
        public void Interact()
        {
            if (!_canInteract) return;
            if (_interactTarget == null) return;
            _interactTarget.Interact();
        }


        private void Update()
        {
            _detectCenterPos = Camera.main.ScreenToWorldPoint(_uiInputReader.MousePositionOnScreen);
            CheckInteract(_detectCenterPos);
        }


        private void CheckInteract(Vector2 position)
        {
            var collider = Physics2D.OverlapCircle(position, _detectRadius, _targetLayer);

            IInteractable newTarget = null;
            bool hasTarget = collider != null && collider.transform.TryGetComponent(out newTarget);

            if (_interactTarget != null && (!hasTarget || _interactTarget != newTarget))
            {
                _interactTarget.HoverExit();
                _interactTarget = null;
            }

            if (hasTarget && _interactTarget != newTarget)
            {
                _interactTarget = newTarget;
                _interactTarget.HoverEnter();
            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_detectCenterPos, _detectRadius);
        }

#endif

    }

}