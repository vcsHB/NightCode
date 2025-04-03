using Combat;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectManage.GimmickObjects.Logics
{
    public class TimerToggle : ToggleObject, IDamageable
    {
        public UnityEvent OnApplyDamage;
        [SerializeField] private float _doorCloseDelay;

        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _disableSprite;

        private bool _isTriggered;
        private float _doorOpenTime;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            if(_isTriggered && _doorOpenTime + _doorCloseDelay < Time.time)
            {
                OnTriggerDisable();
            }
        }

        public bool ApplyDamage(CombatData data)
        {
            _isTriggered = true;
            Debug.Log(_isTriggered);
            _doorOpenTime = Time.time;
            HandleToggle(_isTriggered);
            SetStateSprite();
            OnApplyDamage?.Invoke();
            return true;
        }

        public void OnTriggerDisable()
        {
            _isTriggered = false;
            HandleToggle(_isTriggered);
            SetStateSprite();
        }

        private void SetStateSprite()
        {
            _spriteRenderer.sprite = !_isTriggered ? _activeSprite : _disableSprite;
        }
    }
}
