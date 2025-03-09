using System.Collections.Generic;
using Agents.Players;
using Combat.PlayerTagSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame.GameUI.CharacterSelector
{

    public class CharacterSelectWindow : MonoBehaviour
    {
        [SerializeField] private CharacterSelectSlot _slotPrefab;
        private VerticalLayoutGroup _layoutGroup;
        private RectTransform _contentTrm;

        private Dictionary<int, CharacterSelectSlot> _slotDictionary = new Dictionary<int, CharacterSelectSlot>();

        private void Awake()
        {
            _layoutGroup = GetComponent<VerticalLayoutGroup>();
            _contentTrm = transform as RectTransform;
        }

        public void AddCharacterSlot(PlayerSO playerSO, Player player)
        {
            CharacterSelectSlot slot = Instantiate(_slotPrefab, _contentTrm);
            slot.SetCharacterData(playerSO, player);
            _slotDictionary.Add(playerSO.id, slot);
            LayoutRebuilder.MarkLayoutForRebuild(_contentTrm);
        }

        public void SelectCharacter(int characterID)
        {

            if (_slotDictionary.TryGetValue(characterID, out CharacterSelectSlot slot))
            {
                DisableSelectAllCharacter(characterID);
                slot.Select(true);
            }
            else
            {
                Debug.LogWarning($"Can't Find Character. id: {characterID}");
            }
        }

        private void DisableSelectAllCharacter(int exceptionID = -1)
        {
            foreach (var slot in _slotDictionary)
            {
                if (slot.Key == exceptionID) continue;

                slot.Value.Select(false);
            }
        }



    }
}
