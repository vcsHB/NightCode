using System;
using System.Collections;
using PerformanceSystem.CutScene;
using UnityEngine;

namespace Dialog
{
    public class CutSceneDialogPlayer : InGameDialogPlayer
    {
        [SerializeField] private CutSceneDirector _director;
        [SerializeField] private bool _startOnAwake;
        private bool _isDirectorStopped = false;

        protected override void Awake()
        {
            base.Awake();
            if (_startOnAwake)
                StartDialog();
        }

        public override void StartDialog()
        {
            _curReadingNode = _dialog.nodes[0];
            _director.StartTimeline();
        }

        public virtual void PauseDirector()
        {
            _director.PauseTimeline();
            print("Pause");
            _isDirectorStopped = true;
        }

        public virtual void ReadNode()
        {
            ReadSingleLine();
           
        }

        public virtual void ReadNodeWithPause()
        {
            PauseDirector();
            ReadNode();
        }

        protected override IEnumerator WaitNodeRoutine(Func<bool> waitPredict)
        {
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => _isDirectorStopped);
            yield return new WaitUntil(waitPredict);

            if (_curReadingNode is NormalNodeSO)
            {
                _playingEndAnimation = true;
                CompleteNodeAnim(_curReadingNode);
            }
            else
            {
                _playingEndAnimation = false;
            }

            yield return new WaitUntil(() => _playingEndAnimation == false);

            _curReadingNode.endDialogEvent.ForEach(dialogEvent => dialogEvent.PlayEvent(this, _currentActor));
            yield return new WaitUntil(() => _curReadingNode.endDialogEvent.Exists(dialogEvent => dialogEvent.isCompleteEvent == false) == false);

            _waitCompleteAction?.Invoke();
            _curReadingNode = _nextNode;
            _isReadingDialog = false;

            if (_optionTalk)
            {
                _option.Close();
                _optionTalk = null;
            }

            _director.ResumeTimeline();
            _isDirectorStopped = false;
        }
    }
}
