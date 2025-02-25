using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class RoomUI : MonoBehaviour
    {
        [SerializeField] private RoomInfoUI _roomInfoUI;
        private BasementRoom _roomInfo;

        public void SetRoom(BasementRoom room)
        {
            _roomInfo = room;
        }

        private void OnClickRoomInfoUI()
        {
            _roomInfoUI.SetRoomSO(_roomInfo.roomSO);
            _roomInfoUI.Open();
        }

        public void Open()
        {

        }

        public void Close()
        {

        }
    }
}
