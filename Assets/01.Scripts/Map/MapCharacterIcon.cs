using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Map
{
    public class MapCharacterIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public event Action<MapCharacterIcon> onPointerUp;
        public event Action<MapCharacterIcon> onReturnOriginNode;

        public CharacterIconSO characterIcon;
        private Vector2 _offset;
        private CanvasGroup _canvasGroup;
        private Image _image;

        private CharacterEnum _character;
        private bool _isMoved = false;

        private RectTransform RectTrm => transform as RectTransform;
        public CharacterEnum Character => _character;


        private void Awake()
        {
            _image = GetComponent<Image>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        #region EventRegion

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = Vector3.one * 1.05f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isMoved) return;
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                RectTrm.anchoredPosition = eventData.position + _offset;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isMoved) return;
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
                onPointerUp?.Invoke(this);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && _isMoved == false)
            {
                _offset = RectTrm.anchoredPosition - eventData.position;
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            }

            if (eventData.button == PointerEventData.InputButton.Right && _isMoved)
            {
                ReturnToPrevNode();
            }
        }

        #endregion

        private void ReturnToPrevNode()
        {
            if (_isMoved == false) return;
            onReturnOriginNode?.Invoke(this);
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void SetCharacter(CharacterEnum character)
        {
            _character = character;
            _image.sprite = characterIcon.GetIcon(character);
        }

        public void SetMoved(bool isMoved) => _isMoved = isMoved;
    }
}
