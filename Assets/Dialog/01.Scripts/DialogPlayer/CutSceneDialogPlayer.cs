using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

namespace Dialog
{
    public class CutSceneDialogPlayer : InGameDialogPlayer
    {
        [SerializeField] private PlayableDirector _director;
        private bool _isDirectorStopped = false;

        protected override void Awake()
        {
            base.Awake();
            _director = GetComponent<PlayableDirector>();
        }

        public override void StartDialog()
        {
            _director.Play();
        }

        public virtual void PauseDirector()
        {
            _director.Pause();
            _isDirectorStopped = true;
        }

        public virtual void ReadNode()
        {
            ReadSingleLine();
        }

        public virtual void ReadNodeWithPause()
        {
            ReadNode();
            PauseDirector();
        }

        protected override IEnumerator WaitNodeRoutine(Func<bool> waitPredict, Action endAction)
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

            endAction?.Invoke();
            _curReadingNode = _nextNode;
            _isReadingDialog = false;

            if (_optionTalk)
            {
                _option.Close();
                _optionTalk = null;
            }

            _director.Resume(); 
            _isDirectorStopped = false;
        }
    }
}
