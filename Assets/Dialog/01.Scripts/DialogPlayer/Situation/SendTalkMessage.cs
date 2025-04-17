using UnityEngine;
namespace Dialog.SituationControl
{

    public class SendTalkMessage : SituationElement
    {
        [SerializeField] private string _sender;
        [SerializeField] private string _content;
        private InGameDialogPlayer _dialoguePlayer;
        private void Awake()
        {
            //_dialoguePlayer = InGameDialogPlayer.Instance as InGameDialogPlayer;
        }

        public override void StartSituation()
        {
            //_dialoguePlayer.SendTalkMessage(_sender, _content);
        }
        public override void EndSituation()
        {
        }
    }
}