using Core;
using Core.DataControl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ObjectManage.GimmickObjects
{
    public class GoalPosition : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //일단 바로 넘어가게 해여
            DataLoader.Instance.CompleteMap();
            SceneManager.LoadScene(SceneName.MapSelectScene);
        }
    }
}
