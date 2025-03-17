using UnityEngine;
using UnityEngine.EventSystems;

namespace Basement
{
    public class BasementBuildUI : MonoBehaviour
    {
        [SerializeField] private BuildConfirmPanel buildConfirmPanel;
        [SerializeField] private BasementRoomSO _roomSO;
        [SerializeField] private int _floor;
        [SerializeField] private int _roomNumber;
        [SerializeField] private Color _openColor, _closeColor;
        [SerializeField] private SpriteRenderer _sr;
        private bool _isMouseDown = false;
        private bool _isOpen = false;

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
            if (_isMouseDown)
                OnClick();

            _isMouseDown = false;
        }

        #endregion

        private void OnClick()
        {
            Debug.Log(_isOpen);
            if (_isOpen == false || EventSystem.current.IsPointerOverGameObject()) return;

            BuildConfirmPanel confirmPanel = UIManager.Instance.buildConfirmPanel;
            confirmPanel.SetRoom(_roomSO, Build);
            confirmPanel.Open();
        }

        public void Build()
        {
            if (CheckResource() == false) return;

            BasementManager.Instance.CreateRoom(_roomSO, _floor, _roomNumber);
            UseResource();

            gameObject.SetActive(false);
        }

        private bool CheckResource()
           => BasementManager.Instance.GetMoney() >= _roomSO.requireMoney;

        private void UseResource()
            => BasementManager.Instance.UseResource(_roomSO.requireMoney);

        public void Open()
        {
            _sr.color = _openColor; 
            _isOpen = true;
        }

        public void Close()
        {
            _sr.color = _closeColor;
            _isOpen = false;
        }
    }
}
