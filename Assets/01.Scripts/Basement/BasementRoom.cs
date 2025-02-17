using Basement.CameraController;
using Basement.Player;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace Basement
{
    public abstract class BasementRoom : MonoBehaviour
    {
        public FurnitureSetSO furnitureSet;
        public BasementRoomType roomType;

        [HideInInspector]
        public List<Furniture> furnitureList;
        [HideInInspector]
        public List<Furniture> notSaveFurniture;

        [SerializeField] private Transform _cameraFocusTarget;
        private Transform _originFollow;

        protected virtual void Awake()
        {
            furnitureList = new List<Furniture>();
            notSaveFurniture = new List<Furniture>();
        }

        #region Factor

        /// <summary>
        /// Do only about furniture
        /// If you use other factor you have to convert string what you use and furnitures with space bar
        /// </summary>
        /// <param name="factor"></param>
        public virtual void SetFactor(string factor)
        {
            if (factor == "-") return;

            furnitureList = new List<Furniture>();

            string[] str = factor.Split(',');

            for (int i = 0; i < str.Length; i += 3)
            {
                int id = int.Parse(str[i]);
                Vector2 position = new Vector2(float.Parse(str[i + 1]), float.Parse(str[i + 2]));

                FurnitureSO furnitureSO = furnitureSet.GetFurniture(id);

                Furniture furniture = Instantiate(furnitureSO.furniturePrefab, transform);
                furniture.transform.SetLocalPositionAndRotation(position, Quaternion.identity);

                furnitureList.Add(furniture);
            }
        }

        /// <summary>
        /// Do only about furniture
        /// If you use other factor you have to convert string what you use and furnitures with space bar
        /// </summary>
        /// <returns></returns>
        public virtual string GetFactor()
        {
            if (furnitureList.Count == 0) return "-";

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < furnitureList.Count; i++)
            {
                Furniture furniture = furnitureList[i];

                if (i != 0) sb.Append(",");
                sb.Append(furniture.furnitureSO.furnitureID);
                sb.Append(",");
                sb.Append(furniture.transform.localPosition.x);
                sb.Append(",");
                sb.Append(furniture.transform.localPosition.y);
            }

            return sb.ToString();
        }

        #endregion


        #region FurnitureRegion

        public virtual void AddFurniture(Furniture furniture)
        {
            furniture.Init(this);
            furnitureList.Add(furniture);
        }

        public virtual Furniture AddFurniture(FurnitureSO furniture, Vector2 position)
        {
            Furniture furnitureInstance = Instantiate(furniture.furniturePrefab, transform);
            furnitureInstance.SetPosition(position);
            furnitureInstance.Init(this);

            furnitureList.Add(furnitureInstance);
            return furnitureInstance;
        }

        #endregion


        #region InputRegion

        protected virtual void OnMouseUp()
        {
            if (BasementCameraManager.Instance.CameraMode == CameraMode.Basement) return;

            OpenFurnitureUI();
        }

        #endregion

        private void OpenFurnitureUI()
        {
            _originFollow = BasementCameraManager.Instance.GetCameraFollow();

            BasementCameraManager.Instance.Zoom(1.5f, 0.4f);
            BasementCameraManager.Instance.ChangeFollow(_cameraFocusTarget, 0.3f, () =>
            {
                FurnitureUI ui = UIManager.Instance.GetUIPanel(UIType.FurnitureUI) as FurnitureUI;
                ui.Open(Vector2.zero);
                ui.Init(this);
            });
        }

        public void FocusCamera()
        {
            _originFollow = BasementCameraManager.Instance.GetCameraFollow();
            BasementCameraManager.Instance.ChangeFollow(_cameraFocusTarget, 0.3f, null);
            BasementCameraManager.Instance.Zoom(1.5f, 0.4f);
        }

        public void ReturnFocus()
        {
            BasementCameraManager.Instance.ChangeFollow(_originFollow, 0.3f, null);
            BasementCameraManager.Instance.Zoom(4f, 0.4f);
        }

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            FocusCamera();
        }

        public virtual void OnTriggerExit2D(Collider2D collision)
        {
            ReturnFocus();
        }

    }

    [SerializeField]
    public struct FurnitureInfo
    {
        public int furnitureId;
        public Vector2 furniturePosition;
    }
}
