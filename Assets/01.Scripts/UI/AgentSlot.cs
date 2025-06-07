using Chipset;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class AgentSlot : MonoBehaviour, IPointerClickHandler
    {
        public ChipsetTable chipsetTable;
        public CharacterEnum character;

        public void OnPointerClick(PointerEventData eventData)
        {
            chipsetTable.SelectInventory(character);
        }
    }
}
