using UnityEngine;

namespace Dialog
{
    [RequireComponent(typeof(AnimationPlayer))]
    public abstract class DialogPlayer : MonoBehaviour
    {
        public DialogSO dialog;
        [HideInInspector]public bool stopReading = false;
        protected NodeSO _curReadingNode;
        protected Coroutine _readingNodeRoutine;
        protected bool _playingEndAnimation = false;
        protected bool _isReadingDialog = false;
        
        [SerializeField] protected float _textOutDelay;
        [SerializeField] protected float _nextNodeDealy;

        public bool PlayingEndAnimation => _playingEndAnimation;

        public abstract void StartDialog();
        public abstract void EndDialog();
        public abstract void ReadSingleLine();

        public virtual void CompleteEndAnimation() => _playingEndAnimation = false;

        protected virtual bool GetInput()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}
