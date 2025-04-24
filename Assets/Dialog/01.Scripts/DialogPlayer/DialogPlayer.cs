using System;
using System.Collections.Generic;
using InputManage;
using UnityEngine;

namespace Dialog
{
    [RequireComponent(typeof(AnimationPlayer))]
    public abstract class DialogPlayer : MonoBehaviour
    {
        public Action OnDialogueStart;
        public Action OnDialogueEnd;

        [SerializeField] protected UIInputReader _uiInputReader;

        public DialogSO dialog;
        [HideInInspector] public bool stopReading = false;

        protected NodeSO _curReadingNode;
        protected Coroutine _readingNodeRoutine;
        protected bool _playingEndAnimation = false;
        protected bool _isReadingDialog = false;

        [SerializeField] protected float _textOutDelay;
        [SerializeField] protected float _nextNodeDelay;

        [Header("TalkBubble Pooling")]
        [SerializeField] private TalkBubble _talkBubblePrefab;
        private Queue<TalkBubble> _talkBubblePool = new();
        private List<TalkBubble> _enabledBubbles = new();

        public float TextOutDelay => _textOutDelay;
        public bool PlayingEndAnimation => _playingEndAnimation;

        public abstract void StartDialog();
        public abstract void EndDialog();
        public abstract void ReadSingleLine();

        public virtual void CompleteEndAnimation() => _playingEndAnimation = false;
        public virtual void SetTextOutDelay(float delay) => _textOutDelay = delay;
        private bool _isInputDetected;
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
            _isInputDetected = true;
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

        public virtual void SetDialogueData(DialogSO data)
        {
            dialog = data;
        }

        public TalkBubble GetTalkBubble()
        {
            TalkBubble newBubble = _talkBubblePool.Count > 0
                    ? _talkBubblePool.Dequeue()
                    : Instantiate(_talkBubblePrefab, transform);

            _enabledBubbles.Add(newBubble);
            return newBubble;
        }

        public void RemoveTalkbubble(TalkBubble talkBubble)
        {
            if (_enabledBubbles.Contains(talkBubble))
                _enabledBubbles.Remove(talkBubble);
            _talkBubblePool.Enqueue(talkBubble);
        }

    }
}
