using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.GameSelectScene.CharacterSetting
{
    public class AgentSelectController : MonoBehaviour
    {
        private List<AgentSlot> _agentSlots;
        private Dictionary<CharacterEnum, AgentSlot> _agentSlotDictionary;

        private void Start()
        {
            _agentSlots = GetComponentsInChildren<AgentSlot>().ToList();
            _agentSlotDictionary = new Dictionary<CharacterEnum, AgentSlot>();

            for (int i = 0; i < _agentSlots.Count; i++)
            {
                _agentSlotDictionary.Add(_agentSlots[i].character, _agentSlots[i]);
                _agentSlots[i].OnSelectCharacter += HandleSelectCharacter;
            }

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if (_agentSlotDictionary[character].isRetired == false)
                {
                    _agentSlotDictionary[character].SetSelection(true);
                    break;
                }
            }
        }

        private void HandleSelectCharacter(CharacterEnum character)
        {
            foreach (var slot in _agentSlots)
            {
                if (slot.character != character)
                    slot.SetSelection(false);
            }
        }
    }
}
