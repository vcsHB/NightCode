using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Dialog.SituationControl
{
    
    public class Situation : MonoBehaviour
    {
        public UnityEvent OnDialogueEndEvent;
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
            _dialoguePlayer.OnDialogueEnd += HandleDialogueOver;
        }

        public void SetSituation()
        {
            _dialoguePlayer.SetDialogueData(_dialogScript);
            _dialoguePlayer.SetCharacters(_characters);
        }

        private void HandleDialogueOver()
        {
            _dialoguePlayer.OnDialogueEnd -= HandleDialogueOver;
            OnDialogueEndEvent?.Invoke();
        }
    }
}