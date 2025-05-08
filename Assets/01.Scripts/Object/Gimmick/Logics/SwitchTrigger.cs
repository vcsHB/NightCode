using Combat;
using UnityEngine;

namespace ObjectManage.GimmickObjects.Logics
{

    public class SwitchTrigger : TriggerObject, IDamageable
    {
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _disableSprite;
        private bool _isTriggered;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public bool ApplyDamage(CombatData data)
        {
            if (_isTriggered) return false;
            _isTriggered = true;
            HandleTrigger();
            SetStateSprite();
            return true;
        }

        public void OnTriggerDisable()
        {
            _isTriggered = false;
            HandleTrigger();
            SetStateSprite();
        }

        private void SetStateSprite()
        {
            _spriteRenderer.sprite = !_isTriggered ? _activeSprite : _disableSprite;
        }
    }

}