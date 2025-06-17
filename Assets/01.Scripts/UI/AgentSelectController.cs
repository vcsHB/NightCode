using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.GameSelectScene.CharacterSetting
{
    public class AgentSelectController : MonoBehaviour
    {
        private List<AgentSlot> _agentSlots;
        private Dictionary<CharacterEnum, AgentSlot> _agentSlotDictionary;

        private void Awake()
        {
            _agentSlots = GetComponentsInChildren<AgentSlot>().ToList();
            _agentSlotDictionary = new Dictionary<CharacterEnum, AgentSlot>();

            for (int i = 0; i < _agentSlots.Count; i++)
            {
                _agentSlotDictionary.Add(_agentSlots[i].character, _agentSlots[i]);
                _agentSlots[i].OnSelectCharacter += HandleSelectCharacter;
            }

            _agentSlotDictionary[CharacterEnum.An].SetSelection(true);
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
