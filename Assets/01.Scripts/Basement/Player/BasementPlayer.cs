using Agents;
using InputManage;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = InputManage.PlayerInput;

namespace Basement.Player
{
    public class BasementPlayer : Agent
    {
        [field: SerializeField] public PlayerInput playerInput { get; private set; }
        [SerializeField] private GameObject _pressFBtn;
        [SerializeField] private Transform _visualTrm;
        private SpriteRenderer _renderer;
        private Animator _visualAnimator;
        public event Action OnInteract;

        private Rigidbody2D _rigid;
        private float _xVelocity;
        private bool _wallDetected;
        private bool _readyInteract;
        private int _moveAnimHash = Animator.StringToHash("Move");

        protected override void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _renderer = _visualTrm.GetComponent<SpriteRenderer>();
            _visualAnimator = _visualTrm.GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            Move();
            Interact();
        }

        private void Move()
        {
            float preveDir = _xVelocity;
            _xVelocity = playerInput.InputDirection.x;

            _visualAnimator.SetBool(_moveAnimHash, Mathf.Abs(_xVelocity) > 0);
            _visualTrm.localScale = new Vector3(_xVelocity == 0 ? _visualTrm.localScale.x : _xVelocity, 1, 1);
            _rigid.linearVelocityX = _xVelocity * 5f;
        }

        private void Interact()
        {
            //나중에 뉴인풋에서 키 추가해서 바꿔줘야함
            //Update에서 실행시키지 말고 구독으로 관리하게 바꿔주
            if (Keyboard.current.fKey.wasPressedThisFrame && _readyInteract)
            {
                OnInteract?.Invoke();
                _readyInteract = false;
            }
        }

        public void SetSortingLayer(int layer)
        {
            _renderer.sortingOrder = layer;
        }

        public void SetInteractAction(Action onInteract)
        {
            _readyInteract = true;
            OnInteract += onInteract;
            _pressFBtn.SetActive(true);
            Debug.Log("밍");
        }

        public void RemoveInteractAction(Action onInteract)
        {
            _readyInteract = false;
            OnInteract -= onInteract;

            //if (OnInteract?.GetInvocationList().Length <= 0)
            _pressFBtn.SetActive(false);
        }
    }
}
