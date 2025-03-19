using System;
using InputManage;
using UnityEngine;

namespace Dialog
{
    [RequireComponent(typeof(AnimationPlayer))]
    public abstract class DialogPlayer : MonoBehaviour
    {
        [SerializeField] protected UIInputReader _uiInputReader;
        public DialogSO dialog;
        [HideInInspector]public bool stopReading = false;
        protected NodeSO _curReadingNode;
        protected Coroutine _readingNodeRoutine;
        protected bool _playingEndAnimation = false;
        protected bool _isReadingDialog = false;
        
        [SerializeField] protected float _textOutDelay;
        [SerializeField] protected float _nextNodeDelay;

        public float TextOutDelay => _textOutDelay;
        public bool PlayingEndAnimation => _playingEndAnimation;

        public abstract void StartDialog();
        public abstract void EndDialog();
        public abstract void ReadSingleLine();

        public virtual void CompleteEndAnimation() => _playingEndAnimation = false;
        public virtual void SetTextOutDelay(float delay) => _textOutDelay = delay;
        private bool _isInputDetected;
        protected virtual void Awake()
        {
            _uiInputReader.OnSpaceEvent += HandleMoveToNextDialogue;
        }
        void OnDestroy()
        {
            
            _uiInputReader.OnSpaceEvent -= HandleMoveToNextDialogue;
        }

        private void HandleMoveToNextDialogue()
        {
            _isInputDetected = true;
        }

        protected virtual bool GetInput()
        {
            if(_isInputDetected) 
            {
                Debug.Log("asdasd");
                _isInputDetected = false;
                return true;
            }
            return false;
        }
    }
}
