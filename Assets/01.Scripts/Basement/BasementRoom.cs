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

        protected bool _isFurnitureSetting = false;

        [SerializeField] private Transform _cameraFocusTarget;
        [SerializeField] private float _zoomInValue = 1.5f;
        private float _originZoomValue;
        private Transform _originFollow;
        private BasementController _basement;
        private Collider2D _collider;

        public bool IsFurnitureSettingMode => _isFurnitureSetting;
        public BasementController BasementController => _basement;

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider2D>();
            furnitureList = new List<Furniture>();
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
}
