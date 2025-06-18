using Core;
using Core.DataControl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ObjectManage.GimmickObjects
{
    public class GoalPosition : MonoBehaviour
    {
        public GameObject interactIcon;

        public void Enter()
        {
            interactIcon.SetActive(true);
        }

        public void Exit()
        {
            interactIcon.SetActive(false);
        }

        public void ClearGame()
        {
            DataLoader.Instance.CompleteMap();
            SceneManager.LoadScene(SceneName.MapSelectScene);
        }
    }
}
