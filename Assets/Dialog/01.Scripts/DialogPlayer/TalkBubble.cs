using System;
using Combat.PlayerTagSystem;
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

        public bool IsEnable { get; private set; }

        public void SetEnable(bool value)
        {
            IsEnable = value;

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
            transform.position = (Vector2)_ownerTrm.position + _offset;
        }
    }
}