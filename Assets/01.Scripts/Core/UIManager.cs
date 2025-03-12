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
        public Dictionary<BasementRoomType, IWindowPanel> basementRoomUI;

        public BuildConfirmPanel buildConfirmPanel;
        public BasementTimerUI timer;
        public FurnitureUI furnitureUI;
        public MSGText msgText;
        public RoomUI roomUI;

        protected override void Awake()
        {
            base.Awake();

            basementRoomUI = new Dictionary<BasementRoomType, IWindowPanel>();

            IWindowPanel trainingUI = FindFirstObjectByType<TrainingUI>().GetComponent<IWindowPanel>();
            IWindowPanel lodgingUI = FindFirstObjectByType<LodgingUI>().GetComponent<IWindowPanel>();
            IWindowPanel cafeUI = FindFirstObjectByType<CafeUI>().GetComponent<IWindowPanel>();
            IWindowPanel officeUI = FindFirstObjectByType<OfficeUI>().GetComponent<IWindowPanel>();

            basementRoomUI.Add(BasementRoomType.TrainingRoom, trainingUI);
            basementRoomUI.Add(BasementRoomType.Lodging, lodgingUI);
            basementRoomUI.Add(BasementRoomType.Cafe, cafeUI);
            basementRoomUI.Add(BasementRoomType.Office, officeUI);
        }

        public IWindowPanel GetUIPanel(BasementRoomType uiType) 
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
