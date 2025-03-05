using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class FurnitureUI : MonoBehaviour, IUIPanel
    {
        [SerializeField] private ExsistFurnitureIndicator _furnitureIndicator;
        [SerializeField] private AddFurnitureUI _addFurnitureUI;
        [SerializeField] private Button _closeBtn;
        private BasementRoom _room;

        public void Init(BasementRoom room)
        {
            _room = room;
            _furnitureIndicator.SetBasementRoom(room);
            _addFurnitureUI.Init(room, _furnitureIndicator);
        }

        public void Open(Vector2 position)
        {
            _addFurnitureUI.Open();
            _furnitureIndicator.Open();
            _closeBtn.gameObject.SetActive(true);
        }

        public void Close()
        {
            _addFurnitureUI.Close();
            _furnitureIndicator.Close();
            _closeBtn.gameObject.SetActive(false);
            _room?.ReturnFocus();
        }
    }
}
