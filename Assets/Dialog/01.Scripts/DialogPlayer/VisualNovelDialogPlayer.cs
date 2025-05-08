using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

namespace Dialog
{
    public class VisualNovelDialogPlayer : DialogPlayer
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private IWindowPanel _bgPanel;

        public override void StartDialog()
        {
            _isReadingDialog = true;
            _curReadingNode = _dialog.nodes[0];
            ReadSingleLine();
        }

        public override void EndDialog()
        {
            _isReadingDialog = false;
            _curReadingNode = null;
        }

        public override void ReadSingleLine()
        {
            if(_curReadingNode is NormalNodeSO normal)
            {

            }
            else if(_curReadingNode is OptionNodeSO option)
            {

            }
            else if(_curReadingNode is BranchNodeSO branch)
            {

            }
        }

        protected override IEnumerator ReadingNodeRoutine()
        {
            yield return null;
        }
    }
}
