using Chipset;
using Map;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class AgentSlot : MonoBehaviour, IPointerClickHandler
    {
        public event Action<CharacterEnum> OnSelectCharacter;

        public MapController characterController;
        public ChipsetTable chipsetTable;
        public CharacterEnum character;
        public GameObject retirePanel;

        [SerializeField] private float _selectionAlpha = 1f;
        [SerializeField] private float _unSelectionAlpha = 0.3f;

        private bool _isRetired;
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _isRetired = !characterController.MapGraph.IsCharacterExsists(character);
            retirePanel.SetActive(_isRetired);
        }

        public void SetSelection(bool isSelected)
        {
            if (_isRetired) return;

            if(isSelected) chipsetTable.SelectInventory(character);
            _canvasGroup.alpha = isSelected ? _selectionAlpha : _unSelectionAlpha;
            OnSelectCharacter?.Invoke(character);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SetSelection(true);
        }
    }
}
