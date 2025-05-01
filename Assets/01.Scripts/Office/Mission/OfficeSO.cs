using Dialog;
using UnityEngine;

namespace Core.StageController
{
    [CreateAssetMenu(menuName = "SO/Stage/Office")]
    public class OfficeSO : StageSO
    {
        public bool cutScene;
        public DialogSO cutSceneDialog;
    }
}
