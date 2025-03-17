using Basement.CameraController;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

namespace Basement
{
    public abstract class BasementRoom : MonoBehaviour
    {
        public BasementRoomType roomType;
        public List<Furniture> furnitureList;
        public BasementRoomSO roomSO;

        protected bool _isCharacterSelected = false;
        protected CharacterEnum _selectedCharacter;
        protected bool _isFocusMode = false;
        protected BasementUI _connectedUI;

        #region 

        [SerializeField] private Transform _cameraFocusTarget;
        [SerializeField] private float _zoomInValue = 1.5f;
        private BasementController _basement;
        private Transform _originFollow;
        private Collider2D _collider;
        private FurnitureUI _furnitureUI;
        private RoomUI _roomUI;

        #endregion

        protected FurnitureUI FurnitureUI
        {
            get
            {
                if (_furnitureUI == null)
                    _furnitureUI = UIManager.Instance.furnitureUI;
                return _furnitureUI;
            }
        }
        protected RoomUI RoomUI
        {
            get
            {
                if (_roomUI == null)
                    _roomUI = UIManager.Instance.roomUI;
                return _roomUI;
            }
        }
        private Collider2D Collider
        {
            get
            {
                if (_collider == null)
                    _collider = GetComponent<Collider2D>();
                return _collider;
            }
        }
        public BasementController BasementController => _basement;
        public bool IsUIOpend
        {
            get
            {
                if (_connectedUI == null) return false;
                return _connectedUI.isOpend;
            }
        }


        protected virtual void Awake()
        {
            furnitureList = new List<Furniture>();
        }

        protected void BasmentModeChangeOnFocusMode(BasementMode mode)
        {
            if (_isFocusMode == false) return;

            if (mode == BasementMode.Basement)
            {
                FurnitureUI.Close();
                OpenRoomUI();
            }
            else if (mode == BasementMode.Build)
            {
                RoomUI.Close();
                CloseUI();
                FurnitureSetting();
            }
            _isFocusMode = true;
        }

        public abstract void CloseUI();

        public void ReturnButtonCloseAllUI()
        {
            //UIManager.Instance.returnButton.ChangeReturnAction(RoomUI.Close);
        }

        public void FocusCamera()
        {
            Transform targetTrm = BasementCameraManager.Instance.GetCameraFollow();
            if (targetTrm != _cameraFocusTarget) _originFollow = targetTrm;

            _basement.OnFocusRoom(this);
            BasementCameraManager.Instance.ChangeFollow(_cameraFocusTarget, 0.2f, null);
            BasementCameraManager.Instance.Zoom(_zoomInValue, 0.3f);
            Collider.enabled = false;
            _isFocusMode = true;
        }

        public virtual void ReturnFocus()
        {
            BasementCameraManager.Instance.ChangeFollow(_originFollow, 0.2f, null);
            BasementCameraManager.Instance.ZoomOut(0.3f);
            _collider.enabled = true;
            _isFocusMode = false;
            CloseUI();
        }

        public virtual void PlaceCharacter(CharacterEnum charcter)
        {
            _selectedCharacter = charcter;
            _isCharacterSelected = true;
        }

        public virtual void RemoveCharacter()
        {
            _isCharacterSelected = false;
        }

        public void FurnitureSetting()
        {
            if (_isFocusMode == false)
                FocusCamera();

            FurnitureUI.Init(this);
            FurnitureUI.Open(Vector2.zero);
        }

        public virtual void FocusRoom()
        {
            if (_isFocusMode == false)
                FocusCamera();
            OpenRoomUI();
        }

        public void OpenRoomUI()
        {
            RoomUI.SetRoom(this);
            RoomUI.Open();
        }

        public void OnMouseUp()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            //if (_basement.GetCurrentBasementMode() == BasementMode.Basement) FocusRoom();
            //else FurnitureSetting();
        }

        public void AddFurniture(FurnitureSO furniture, Vector2 position)
        {
            Furniture furnitureInstance = Instantiate(furniture.furniturePrefab, transform);
            furnitureInstance.transform.SetLocalPositionAndRotation(position, Quaternion.identity);
            furnitureInstance.Init(this);
            furnitureList.Add(furnitureInstance);
        }

        public virtual void Init(BasementController basement)
        {
            _basement = basement;
            _basement.OnChangeBasmentMode += BasmentModeChangeOnFocusMode;
        }
    }
}
