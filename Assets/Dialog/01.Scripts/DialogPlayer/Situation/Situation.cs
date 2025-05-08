using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialog.SituationControl
{

    public class Situation : MonoBehaviour
    {
        public UnityEvent OnDialogueEndEvent;
        public UnityEvent OnDialogueStartEvent;
        [SerializeField] private float _dialogueStartDelay = 3f;
        [SerializeField] private DialogSO _dialogScript;
        private InGameDialogPlayer _dialoguePlayer;
        private SituationElement[] _elements;

        private void Awake()
        {
            _elements = GetComponentsInChildren<SituationElement>();
        }

        public void Init(InGameDialogPlayer dialogPlayer)
            => _dialoguePlayer = dialogPlayer;

        public void SetDialogSO(DialogSO dialog)
            => _dialogScript = dialog;


        [ContextMenu("PlayerSituation")]
        public void PlaySituation()
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].StartSituation();
            }
            StartCoroutine(SituationCoroutine());
        }
        
        private IEnumerator SituationCoroutine()
        {
            yield return new WaitForSeconds(_dialogueStartDelay);
            SetSituation();
            _dialoguePlayer.StartDialog();
            OnDialogueStartEvent?.Invoke();
            _dialoguePlayer.OnDialogEnd += HandleDialogueOver;
        }

        public void SetSituation()
        {
            _dialoguePlayer.SetDialog(_dialogScript);
        }

        private void HandleDialogueOver()
        {
            _dialoguePlayer.OnDialogEnd -= HandleDialogueOver;
            OnDialogueEndEvent?.Invoke();
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].EndSituation();
            }
        }
    }
}