using System.Collections.Generic;
using Combat.PlayerTagSystem;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace UI.InGame.GameUI.CharacterSelector
{

    public class CharacterSelectWindow : MonoBehaviour
    { 
        [SerializeField] private CharacterSelectSlot _slotPrefab;

        private List<CharacterSelectSlot> _slotList = new();


        public void AddCharacterSlot(PlayerSO playerSO)
        {
            
        }


        
    }
}
