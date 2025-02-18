using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Android;

namespace Basement
{
    public class BasementBuildUI : MonoBehaviour
    {
        [SerializeField] private BasementRoomSetSO _roomSetSO;
        [SerializeField] private BasementRoomType _roomType;
        [SerializeField] private int _floor;
        [SerializeField] private int _roomNumber;
        private bool _isMouseDown = false;

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

            BasementManager.Instance.CreateRoom(_roomType, _floor, _roomNumber);
        }
    }
}
