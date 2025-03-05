using Basement.CameraController;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;

namespace Basement
{
    public abstract class BasementRoom : MonoBehaviour
    {
        public BasementRoomType roomType;
        public List<Furniture> furnitureList;
        public BasementRoomSO roomSO;

        [SerializeField]protected List<CharacterType> _selectedCharacters;
        protected bool _isFurnitureSetting = false;
        protected bool _isBasementMode = false;

        [SerializeField] private Transform _cameraFocusTarget;
        [SerializeField] private float _zoomInValue = 1.5f;
        private float _originZoomValue;
        private Transform _originFollow;
        private BasementController _basement;
        private Collider2D _collider;

        public bool IsFurnitureSettingMode => _isFurnitureSetting;
        public bool IsBasementMode => _isBasementMode;
        public BasementController BasementController => _basement;

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider2D>();
            furnitureList = new List<Furniture>();
            _selectedCharacters = new List<CharacterType>();

            for(int i = 0; i < roomSO.maxSeatingCapacity; i++)
                _selectedCharacters.Add(CharacterType.Null);
        }

        public void FocusCamera()
        {
            _originFollow = BasementCameraManager.Instance.GetCameraFollow();
            _originZoomValue = BasementCameraManager.Instance.CameraSize;
            BasementCameraManager.Instance.ChangeFollow(_cameraFocusTarget, 0.3f, null);
            BasementCameraManager.Instance.Zoom(_zoomInValue, 0.4f);
            _collider.enabled = false;
        }

        public void ReturnFocus()
        {
            BasementCameraManager.Instance.ChangeFollow(_originFollow, 0.3f, null);
            BasementCameraManager.Instance.Zoom(_originZoomValue, 0.4f);
            _collider.enabled = true;
            _isFurnitureSetting = false;
            _isBasementMode = false;
        }

        public void FurnitureSetting()
        {
            FocusCamera();
            _isFurnitureSetting = true;

            FurnitureUI furnitureUI = UIManager.Instance.GetUIPanel(UIType.FurnitureUI) as FurnitureUI;
            furnitureUI.Init(this);
            furnitureUI.Open(Vector2.zero);
        }

        public virtual void FocusRoom()
        {
            FocusCamera();
            _isBasementMode = true;

            UIManager.Instance.roomUI.SetRoom(this);
            UIManager.Instance.roomUI.Open();
        }

        public virtual void ChangeMode(BasementMode mode)
        {
            if(mode == BasementMode.Basement)
            {
                _isFurnitureSetting = false;
                _isBasementMode = true;
            }

            if(mode == BasementMode.Build)
            {
                _isFurnitureSetting = true;
                _isBasementMode = false;
            }
        }

        public void OnMouseUp()
        {
            if (_basement.GetCurrentMode() == BasementMode.Basement) FocusRoom();
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
