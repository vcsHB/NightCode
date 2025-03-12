using Basement.CameraController;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Basement
{
    public abstract class BasementRoom : MonoBehaviour
    {
        public BasementRoomType roomType;
        public List<Furniture> furnitureList;
        public BasementRoomSO roomSO;

        [SerializeField] protected List<CharacterType> _selectedCharacters;
        protected bool _isFocusMode = false;
        protected FurnitureUI _furnitureUI;
        protected RoomUI _roomUI;

        [SerializeField] private Transform _cameraFocusTarget;
        [SerializeField] private float _zoomInValue = 1.5f;
        private BasementController _basement;
        private float _originZoomValue =999;
        private Transform _originFollow;
        private Collider2D _collider;

        public BasementController BasementController => _basement;

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider2D>();
            furnitureList = new List<Furniture>();
            _selectedCharacters = new List<CharacterType>();

            for (int i = 0; i < roomSO.maxSeatingCapacity; i++)
                _selectedCharacters.Add(CharacterType.Null);
        }

        protected virtual void Start()
        {
            _furnitureUI = UIManager.Instance.furnitureUI;
            _roomUI = UIManager.Instance.roomUI;
        }

        protected void BasmentModeChangeOnFocusMode(BasementMode mode)
        {
            if (_isFocusMode == false) return;

            if (mode == BasementMode.Basement)
            {
                _furnitureUI.Close();
                CloseUI();
                FocusRoom();
            }
            else if (mode == BasementMode.Build)
            {
                _roomUI.Close();
                CloseUI();
                FurnitureSetting();
            }
            _isFocusMode = true;
        }

        public abstract void CloseUI();
        public abstract void OpenUI(); 

        public void ChangeReturnButtonListener(UnityAction onClickReturnBtn)
        {
            _roomUI.returnBtn.onClick.RemoveAllListeners();
            _roomUI.returnBtn.onClick.AddListener(onClickReturnBtn);
        }

        public void ReturnButtonCloseAllUI()
        {
            _roomUI.returnBtn.onClick.RemoveAllListeners();
            _roomUI.returnBtn.onClick.AddListener(_roomUI.Close);
        }

        public void FocusCamera()
        {
            Transform targetTrm = BasementCameraManager.Instance.GetCameraFollow();
            float targetZoomValue = BasementCameraManager.Instance.CameraSize;

            if (targetTrm != _cameraFocusTarget) _originFollow = targetTrm;
            if (targetZoomValue > _zoomInValue) _originZoomValue = targetZoomValue;

            _basement.OnFocusRoom(this);
            BasementCameraManager.Instance.ChangeFollow(_cameraFocusTarget, 0.3f, null);
            BasementCameraManager.Instance.Zoom(_zoomInValue, 0.4f);
            _collider.enabled = false;
            _isFocusMode = true;
        }

        public virtual void ReturnFocus()
        {
            BasementCameraManager.Instance.ChangeFollow(_originFollow, 0.3f, null);
            BasementCameraManager.Instance.Zoom(_originZoomValue, 0.4f);
            _collider.enabled = true;
            _isFocusMode = false;
            CloseUI();
        }


        public void FurnitureSetting()
        {
            if (_isFocusMode == false)
                FocusCamera();

            _furnitureUI.Init(this);
            _furnitureUI.Open(Vector2.zero);
        }

        public virtual void FocusRoom()
        {
            if (_isFocusMode == false)
                FocusCamera();

            OpenUI();
        }

        public void OnMouseUp()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (_basement.GetCurrentBasementMode() == BasementMode.Basement) FocusRoom();
            else FurnitureSetting();
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

    public enum CharacterType
    {
        Null,
        Katana,
        CrecentBlade,
        Cross
    }
}
