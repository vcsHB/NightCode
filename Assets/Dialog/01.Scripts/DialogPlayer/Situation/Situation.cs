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
        [SerializeField] private List<Actor> _characters;
        [SerializeField] private DialogSO _dialogScript;
        [SerializeField] private float _dialogueStartDelay = 3f;
        private InGameDialogPlayer _dialoguePlayer;
        private SituationElement[] _elements;

        [SerializeField] private InGameDialogPlayer debugDialog;

        private void Awake()
        {
            Init(debugDialog);
            _elements = GetComponentsInChildren<SituationElement>();
        }
        private void Start()
        {

            //_dialoguePlayer = DialogPlayer.Instance as InGameDialogPlayer;
        }

        public void Init(InGameDialogPlayer dialogPlayer)
        {
            _dialoguePlayer = dialogPlayer;
        }


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
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].EndSituation();
            }
        }
    }
}