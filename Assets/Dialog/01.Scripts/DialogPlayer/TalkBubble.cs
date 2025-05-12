using System;
using TMPro;
using UnityEngine;
namespace Dialog
{
    public class TalkBubble : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _contentText;
        public event Action OnContentOverEvent;

        public TextMeshProUGUI ContentTextMeshPro => _contentText;
        private Transform _ownerTrm;
        private Vector2 _offset;
        private Vector2 _originScale;

        public bool IsEnable { get; private set; }

        private void Awake()
        {
            _originScale = transform.localScale;
        }

        private void Update()
        {
            transform.localRotation = transform.parent.rotation;
        }

        public void SetEnable(bool value)
        {
            IsEnable = value;
            gameObject.SetActive(value);
        }

        public void SetDisabled()
        {
            SetEnable(false);
        }

        public void SetEnabled()
        {
            SetEnable(true);
        }


        public void ClearContent()
        {
            _contentText.text = String.Empty;
        }

        public void SetOwner(Transform owner, Vector2 offset)
        {
            _ownerTrm = owner;
            _offset = offset;
            transform.parent = owner;
            transform.position = (Vector2)_ownerTrm.position + _offset;
        }
    }
}