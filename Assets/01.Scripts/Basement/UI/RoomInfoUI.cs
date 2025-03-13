using Basement.Training;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Basement
{
    public class RoomInfoUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _explainText;

        private Tween _tween;
        private bool _isOpen = false;
        private bool _isMouseEnter = false;

        private RectTransform _rctTrm => transform as RectTransform;

        public void SetRoomSO(BasementRoomSO roomSO)
        {
            _nameText.SetText(roomSO.roomName);
            _explainText.SetText(roomSO.roomExplain);
        }

        private void Update()
        {
            if (_isOpen && _isMouseEnter == false
                && Mouse.current.leftButton.wasPressedThisFrame)
                Close();
        }

        public void Open()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rctTrm.DOAnchorPosX(0, 0.3f);
            _isOpen = true;
        }

        public void Close()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rctTrm.DOAnchorPosX(500, 0.3f);
            _isOpen = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isMouseEnter = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isMouseEnter = true;
        }
    }
}
