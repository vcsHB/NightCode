using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Basement;
using Basement.Training;
using UI;

namespace Basement
{

    public class UIManager : MonoSingleton<UIManager>
    {
        public Dictionary<BasementRoomType, BasementUI> basementRoomUI;

        public Transform popupTextParent;
        public PopupText popupText;
        public BuildConfirmPanel buildConfirmPanel;
        public BasementTimerUI timer;
        public FurnitureUI furnitureUI;
        //public MSGText msgText;
        public RoomUI roomUI;
        public BasementRoomChangeUI basementUI;

        protected override void Awake()
        {
            base.Awake();

            basementRoomUI = new Dictionary<BasementRoomType, BasementUI>();

            BasementUI trainingUI = FindFirstObjectByType<TrainingUI>();
            BasementUI lodgingUI = FindFirstObjectByType<LodgingUI>();
            //BasementUI cafeUI = FindFirstObjectByType<CafeUI>();
            BasementUI officeUI = FindFirstObjectByType<OfficeUI>();

            basementRoomUI.Add(BasementRoomType.TrainingRoom, trainingUI);
            basementRoomUI.Add(BasementRoomType.Lodging, lodgingUI);
            //basementRoomUI.Add(BasementRoomType.Cafe, cafeUI);
            basementRoomUI.Add(BasementRoomType.Office, officeUI);
        }

        public void SetPopupText(string text, Vector2 position)
        {
            PopupText popup = Instantiate(popupText, popupTextParent);
            popup.RectTrm.anchoredPosition = position;
            popup.SetText(text);
        }

        public BasementUI GetUIPanel(BasementRoomType uiType) 
            => basementRoomUI[uiType];
    }



    public enum UIType
    {
        SkillTreePanel,
        BuildUIPanel,
        CharacterSelectPanel,
        FurnitureUI
        //TrainingPanel
    }
}
