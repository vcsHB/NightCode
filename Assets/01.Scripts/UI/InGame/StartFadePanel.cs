using UnityEngine;
namespace UI.InGame
{

    public class StartFadePanel : UIPanel
    {
        protected override void Awake()
        {
            base.Awake();
            SetCanvasActiveImmediately(true);
            Close();
        }
    }
}