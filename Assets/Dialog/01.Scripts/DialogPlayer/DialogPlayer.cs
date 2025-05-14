using System;
using System.Collections;
using InputManage;
using UnityEngine;

namespace Dialog
{
    [RequireComponent(typeof(AnimationPlayer))]
    public abstract class DialogPlayer : MonoBehaviour
    {
        public Action OnDialogStart;
        public Action OnDialogEnd;
        [HideInInspector] public bool stopReading = false;

        [SerializeField] protected UIInputReader _uiInputReader;
        [SerializeField] protected DialogSO _dialog;

        protected NodeSO _curReadingNode;
        protected Coroutine _readingNodeRoutine;
        protected bool _playingEndAnimation = false;
        protected bool _isReadingDialog = false;

        [SerializeField] protected float _textOutDelay;
        [SerializeField] protected float _nextNodeDelay;
        private bool _isInputDetected;

        public float TextOutDelay => _textOutDelay;
        public bool PlayingEndAnimation => _playingEndAnimation;
        public DialogSO Dialog => _dialog;

        public virtual void StartDialog()
        {
            if (_isReadingDialog)
            {
                Debug.LogWarning("A Dialog is already running in this player\nYou can not run multiple dialog in single player");
                return;
            }

            _isReadingDialog = true;
            _curReadingNode = _dialog.nodes[0];
            ReadSingleLine();
        }

        public virtual void EndDialog()
        {
            _isReadingDialog = false;
            _curReadingNode = null;
            OnDialogEnd?.Invoke();
        }


        public virtual void ReadSingleLine()
        {
            if (_curReadingNode == null)
            {
                EndDialog();
                return;
            }

            StartCoroutine(ReadingNodeRoutine());
            //DialogConditionManager.Instance.CountVisit(_curReadingNode.guid);
        }

        protected abstract IEnumerator ReadingNodeRoutine();


        public virtual void CompleteEndAnimation() => _playingEndAnimation = false;

        public virtual void SetTextOutDelay(float delay) => _textOutDelay = delay;

        protected virtual  void Awake()
        {
            _uiInputReader.OnSpaceEvent += HandleMoveToNextDialogue;
        }
        void OnDestroy()
        {

            _uiInputReader.OnSpaceEvent -= HandleMoveToNextDialogue;
        }

        private void HandleMoveToNextDialogue()
        {
            StartCoroutine(HandelMoveToNextRoutine());
        }

        private IEnumerator HandelMoveToNextRoutine()
        {
            _isInputDetected = true;
            yield return null;
            _isInputDetected = false;
        }

        protected virtual bool GetInput()
        {
            if (_isInputDetected)
            {
                _isInputDetected = false;
                return true;
            }
            return false;
        }

        public virtual void SetDialog(DialogSO data)
        {
            _dialog = data;
        }
    }
}
