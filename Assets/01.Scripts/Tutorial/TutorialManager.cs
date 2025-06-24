using System.Collections;
using Combat.PlayerTagSystem;
using Core.DataControl;
using Core.StageController;
using QuestSystem.LevelSystem;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tutorial
{

    public class TutorialManager : MonoBehaviour
    {

        public void ClearTutotial()
        {
            DataLoader.Instance.GetUserData().isClearTutorial = true;
        }


    }

}