using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace UI.InGame.GameUI.QuestSyetem
{

    public class ProgressDisplayer : MonoBehaviour
    {

        public void SetEnable()
        {
            gameObject.SetActive(true);
        }
        public void SetDisable()
        {
            gameObject.SetActive(false);

        }
        public virtual void SetProgress(float ratio)
        {
        }
    }

}