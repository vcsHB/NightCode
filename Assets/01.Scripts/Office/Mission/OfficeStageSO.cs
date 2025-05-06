using Base.Office;
using Dialog;
using UnityEngine;

namespace Core.StageController
{
    [CreateAssetMenu(menuName = "SO/Stage/Office")]
    public class OfficeStageSO : StageSO
    {
        public OfficeSO officeInfo;
        [Space]
        public bool cutScene;
        public DialogSO cutSceneDialog;
        //public Office
    }
}
