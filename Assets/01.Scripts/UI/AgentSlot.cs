using Chipset;
using Map;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class AgentSlot : MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent<CharacterEnum> SelectCharacterEvent;
        public event Action<CharacterEnum> OnSelectCharacter;

        public MapController characterController;
        public ChipsetTable chipsetTable;
        public CharacterEnum character;
        public GameObject retirePanel;

        [SerializeField] private float _selectionAlpha = 1f;
        [SerializeField] private float _unSelectionAlpha = 0.3f;

        private bool _isRetired;
        private CanvasGroup _canvasGroup;

        public bool isRetired => _isRetired;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _isRetired = !characterController.MapGraph.IsCharacterExsists(character);
            retirePanel.SetActive(_isRetired);
            if (_isRetired) _canvasGroup.alpha = 1;
        }

        public void SetSelection(bool isSelected)
        {
            if (_isRetired) return;
            if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = isSelected ? _selectionAlpha : _unSelectionAlpha;

            if (isSelected)
            {
                chipsetTable.SelectInventory(character);
                OnSelectCharacter?.Invoke(character);
                SelectCharacterEvent?.Invoke(character);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SetSelection(true);
        }
    }
}
