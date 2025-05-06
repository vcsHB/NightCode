using Agents.Animate;
using Base.Entity;
using System;
using UnityEngine;

namespace Base.Office
{
    public class OfficePlayer : MonoBehaviour
    {
        public event Action OnInteract;
        public BaseInput baseInput;

        [SerializeField] private AnimParamSO _moveParam;
        [SerializeField] private GameObject _interactIcon;
        [SerializeField] private float _speed;
        private EntityRenderer _entityRenderer;

        private void Awake()
        {
            _entityRenderer = GetComponentInChildren<EntityRenderer>();
        }

        public void OnEnable()
        {
            baseInput.onInteract += HandleInteract;
        }
        private void OnDisable()
        {
            baseInput.onInteract -= HandleInteract;
        }

        private void HandleInteract()
        {
            OnInteract?.Invoke();
        }

        public void AddInteract(Action onInteract)
        {
            OnInteract += onInteract;
            _interactIcon.SetActive(true);
        }

        public void RemoveInteract(Action onInteract)
        {
            OnInteract -= onInteract;
            _interactIcon.SetActive(false);
        }

        private void FixedUpdate()
        {
            Move(baseInput.MoveDir.x);
        }


        public void Move(float dir)
        {
            transform.position += Vector3.right * dir * _speed * Time.deltaTime;
            _entityRenderer.SetAnimParam(_moveParam.hashValue, (dir != 0));
            _entityRenderer.FlipControl(dir);
        }
    }
}
