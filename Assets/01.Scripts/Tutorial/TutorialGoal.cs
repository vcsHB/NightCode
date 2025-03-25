using UnityEngine;

namespace Tutorial
{
    public class TutorialGoal : MonoBehaviour
    {
        [SerializeField] private string _exitSceneName; 
        [SerializeField] private TutorialManager _tutorialManager;
       

        public void HandleMoveToLobby()
        {
            _tutorialManager.OpenSceneExitPanel();
            _tutorialManager.ExitAndMoveToScene("Cafe_TutorialScene");
            Time.timeScale = 1f;
        }

        
    }


}