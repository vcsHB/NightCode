using Core.DataControl;
using UnityEngine;

namespace Tutorial
{

    public class TutorialManager : MonoBehaviour
    {

        public void ClearTutotial()
        {
            DataLoader.Instance.GetUserData().isClearTutorial = true;
            DataLoader.Instance.Save();
        }


    }

}