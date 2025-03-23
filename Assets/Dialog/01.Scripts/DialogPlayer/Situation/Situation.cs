using System.Collections.Generic;
using UnityEngine;
namespace Dialog.SituationControl
{

    public struct SituationData
    {

    }
    public class Situation : MonoBehaviour
    {
        [SerializeField] private List<Actor> _characters;
        [SerializeField] private DialogSO _dialogScript;
        private InGameDialogPlayer _dialoguePlayer;

        private void Start()
        {
            _dialoguePlayer = DialogPlayer.Instance as InGameDialogPlayer;
        }
        [ContextMenu("PlayerSituation")]
        public void PlaySituation()
        {
            SetSituation();
            _dialoguePlayer.StartDialog();
        }

        public void SetSituation()
        {
            _dialoguePlayer.SetDialogueData(_dialogScript);
            _dialoguePlayer.SetCharacters(_characters);
        }
    }
}