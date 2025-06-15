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
            //�ϴ� �ٷ� �Ѿ�� �ؿ�
            DataLoader.Instance.CompleteMap();
            SceneManager.LoadScene(SceneName.MapSelectScene);
        }
    }
}
