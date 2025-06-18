using Core.StageController;
using Dialog;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Office
{
    [CreateAssetMenu(menuName = "SO/Office/OfficeSO")]
    public class OfficeSO : ScriptableObject
    {
        public StageSO stageToSkip;
        public OfficeNPCSO ANInfo;
        public OfficeNPCSO JinLayInfo;

        [Header("Dialog what you have to play")]
        public List<DialogSO> essentialDialogs;

        public bool CheckReadEssentialDialog()
        {
            foreach (var dialog in essentialDialogs)
            {
                if (dialog == null) continue;
                if (DialogConditionManager.Instance.GetVisit(dialog.FirstNode.guid) < 1) 
                    return false;
            }

            return true;
        }
    }
}
