using Basement.CameraController;
using Basement.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Basement
{
    public abstract class BasementRoom : MonoBehaviour
    {
        public FurnitureSetSO furnitureSet;
        public BasementRoomType roomType;

        [HideInInspector]
        public List<FurnitureInfo> furnitureInfo;

        [SerializeField] private Transform _cameraFocusTarget;
        private Transform _originFollow;
        private bool isMouseDown = false;

        public abstract void SetFactor(string factor);

        public virtual void SetFurniture()
        {
            furnitureInfo.ForEach(furniture =>
            {
                FurnitureSO furnitureSO = furnitureSet.GetFurniture(furniture.furnitureId);
                Furniture furnitureInstance = Instantiate(furnitureSO.furniturePrefab, furniture.furniturePosition, Quaternion.identity);
                //furnitureInstance.Init();
            });
        }

        protected virtual void OnMouseDown()
        {
            if (BasementCameraManager.Instance.CameraMode == CameraMode.Basement) return;
            isMouseDown = true;
        }

        protected virtual void OnMouseUp()
        {
            if (BasementCameraManager.Instance.CameraMode == CameraMode.Basement) return;
            isMouseDown = false;
            FocusCamera();
        }

        public void FocusCamera()
        {
            _originFollow = BasementCameraManager.Instance.GetCameraFollow();
            BasementCameraManager.Instance.ChangeFollow(_cameraFocusTarget, 0.3f, null);
            BasementCameraManager.Instance.Zoom(1.5f, 0.4f);
        }

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            FocusCamera();
        }

        public virtual void OnTriggerExit2D(Collider2D collision)
        {
            BasementCameraManager.Instance.ChangeFollow(_originFollow, 0.3f, null);
            BasementCameraManager.Instance.Zoom(4f, 0.4f);
        }
    }

    [SerializeField]
    public struct FurnitureInfo
    {
        public int furnitureId;
        public Vector2 furniturePosition;
    }
}
