using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Android;

namespace Basement
{
    public class BuildUI : MonoBehaviour
    {
        private int _floor;
        private int _roomNumber;
        private bool _isMouseDown = false;
        private BuildingSelectPanel _buildingSelectPanel;
        [SerializeField] private BasementSO _basementSO;


        #region MouseEvents

        private void OnMouseEnter()
        {
            transform.localScale = Vector3.one * 1.05f;
        }

        private void OnMouseExit()
        {
            transform.localScale = Vector3.one;
        }

        private void OnMouseDown()
        {
            _isMouseDown = true;
        }

        private void OnMouseUp()
        {
            if( _isMouseDown)
                OnClick();
            
            _isMouseDown = false;
        }

        #endregion

        private void OnClick()
        {
            _buildingSelectPanel.Open();
            _buildingSelectPanel.onSelectRoom += OnSelectBuilding;
        }

        public void OnSelectBuilding(BasementRoomType roomType)
        {
            _basementSO.floorInfos[_floor].rooms[_roomNumber].roomType = roomType;

            BasementRoom room = Instantiate(_basementSO.GetBasementRoom(roomType));
            room.transform.position = transform.position;

            _buildingSelectPanel.onSelectRoom -= OnSelectBuilding;
            Destroy(gameObject);
        }

        public void Init(int floor, int roomNumber, BuildingSelectPanel buildingSelectPanel)
        {
            _floor = floor;
            _roomNumber = roomNumber;
            _buildingSelectPanel = buildingSelectPanel;
        }
    }
}
