using System.Collections.Generic;
using Agents.Players;
using Combat.PlayerTagSystem;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace UI.InGame.GameUI.CharacterSelector
{

    public class CharacterSelectWindow : MonoBehaviour
    {
        [SerializeField] private CharacterSelectSlot _slotPrefab;

        private Dictionary<int, CharacterSelectSlot> _slotDictionary = new Dictionary<int, CharacterSelectSlot>();


        public void AddCharacterSlot(PlayerSO playerSO, Player player)
        {
            CharacterSelectSlot slot = Instantiate(_slotPrefab, transform);
            slot.SetCharacterData(playerSO, player);
            _slotDictionary.Add(playerSO.id, slot);
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
            foreach(var slot in _slotDictionary)
            {
                if(slot.Key == exceptionID) continue;

                slot.Value.Select(false);   
            }
        }



    }
}
