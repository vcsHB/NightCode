using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class RoomUI : MonoBehaviour
    {


        private BasementRoom _roomInfo;

        public void SetRoom(BasementRoom room)
        {
            _roomInfo = room;
        }
    }
}
