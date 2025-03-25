using UnityEngine;
using UnityEngine.EventSystems;

namespace Basement
{
    public class BasementBuildUI : IngameInteractiveObject
    {
        [SerializeField] private BuildConfirmPanel buildConfirmPanel;
        [SerializeField] private BasementRoomSO _roomSO;
        [SerializeField] private int _floor;
        [SerializeField] private int _roomNumber;
        [SerializeField] private Color _openColor, _closeColor;
        [SerializeField] private SpriteRenderer _sr;
        private Collider2D _collider;
        private bool _isMouseDown = false;
        private bool _isOpen = false;

        public Collider2D Collider
        {
            get
            {
                if (_collider == null)
                    _collider = GetComponent<Collider2D>();
                return _collider;
            }
        }

        #region MouseEvents

        private void OnMouseEnter()
        {
            transform.localScale = Vector3.one * 1.05f;
        }

        private void OnMouseExit()
        {
            transform.localScale = Vector3.one;
        }

        protected override void OnMouseLeftButtonUp()
        {
            base.OnMouseLeftButtonUp();
            OnClick();
        }

        #endregion

        private void OnClick()
        {
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
            Collider.enabled = true;
            _sr.color = _openColor; 
            _isOpen = true;
        }

        public void Close()
        {
            Collider.enabled = false;
            _sr.color = _closeColor;
            _isOpen = false;
        }
    }
}
