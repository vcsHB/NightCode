using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

namespace Dialog
{
    [RequireComponent(typeof(AnimationPlayer))]
    public class InGameDialogPlayer : DialogPlayer
    {
        private AnimationPlayer _animPlayer;
        private PlayableDirector _director;

        [SerializeField] private DialogOption _option;

        private Actor _currentActor;
        private OptionNodeSO _optionTalk;
        private NodeSO _nextNode;

        protected override void Awake()
        {
            base.Awake();
            _director = GetComponent<PlayableDirector>();
            _animPlayer = GetComponent<AnimationPlayer>();
        }

        #region Animation

        private void LateUpdate()
        {
            if (_curReadingNode is NormalNodeSO node && _isReadingDialog)
            {
                _animPlayer.PlayAnimation(_currentActor.ContentText, node.contentTagAnimations);
            }
        }

        private void InitNodeAnim(NodeSO node)
        {
            List<TagAnimation> anims = node.GetAllAnimations();

            anims.ForEach((anim) =>
            {
                anim.Init();

                if (anim is SpriteAnimation srAnim) srAnim.Init(_currentActor.spriteRenderer);
                if (anim is StopReadingAnimation stopAnim) stopAnim.Init(this);
            });
        }

        private void CompleteNodeAnim(NodeSO node)
        {
            List<TagAnimation> anims = node.GetAllAnimations();
            anims.ForEach((anim) => anim.Complete());
        }

        #endregion

        #region ReadingRoutines

        protected override IEnumerator ReadingNodeRoutine()
        {
            _isReadingDialog = false;

            if (_curReadingNode is NormalNodeSO node)
            {
                DialogActorManager.Instance.TryGetActor(node.GetReaderName(), out _currentActor);
            }

            _curReadingNode.startDialogEvent.ForEach(dialogEvent => dialogEvent.PlayEvent(this, _currentActor));
            yield return new WaitUntil(() => !_curReadingNode.startDialogEvent.Exists(dialogEvent => dialogEvent.isCompleteEvent == false));

            _isReadingDialog = true;
            if (_curReadingNode is NormalNodeSO normal)
            {
                _currentActor?.personalTalkBubble.SetEnabled();
                _readingNodeRoutine = StartCoroutine(ReadingNormalNodeRoutine(normal));
            }
            else if(_curReadingNode is TimelineNodeSO timeline)
            {
                _director.Play(timeline.playable);
                StartCoroutine(WaitNodeRoutine(() => _director.state != PlayState.Playing, null));
            }
            else if (_curReadingNode is OptionNodeSO option)
            {
                ReadingOptionNodeRoutine(option);
            }
            else if (_curReadingNode is BranchNodeSO branch)
            {
                JudgementCondition(branch);
            }
        }

        private IEnumerator ReadingNormalNodeRoutine(NormalNodeSO node)
        {
            TextMeshProUGUI tmp = _currentActor.ContentText;

            tmp.SetText(node.GetContents());
            tmp.maxVisibleCharacters = 0;
            InitNodeAnim(node);
            _isReadingDialog = true;
            while (tmp.maxVisibleCharacters < tmp.text.Length)
            {
                if (tmp.text[tmp.maxVisibleCharacters++] == ' ') continue;

                yield return new WaitForSeconds(_textOutDelay);
                yield return new WaitUntil(() => stopReading == false);
            }
            _nextNode = node.nextNode;
            StartCoroutine(WaitNodeRoutine(GetInput, _currentActor.OnCompleteNode));
        }

        private void ReadingOptionNodeRoutine(OptionNodeSO node)
        {
            InitNodeAnim(node);
            _option.SetOption(node, OnSelectOption);
            //StartCoroutine(WaitNodeRoutine(() => _optionSelected, null));
        }

        private void OnSelectOption(Option option)
        {
            _playingEndAnimation = false;

            _optionTalk = _curReadingNode as OptionNodeSO;
            _curReadingNode = ScriptableObject.CreateInstance<NormalNodeSO>();
            (_curReadingNode as NormalNodeSO).SetNormalNodeByOption(option);
            ReadSingleLine();
        }

        private void JudgementCondition(BranchNodeSO branch)
        {
            bool decision = branch.condition.Decision();
            _curReadingNode = branch.nextNodes[decision ? 0 : 1];
            _playingEndAnimation = false;
            ReadSingleLine();
        }

        private IEnumerator WaitNodeRoutine(Func<bool> waitPredict, Action endAction)
        {
            yield return new WaitForSeconds(0.1f);
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

            if(_optionTalk)
            {
                _option.Close();
                _optionTalk = null;
            }

            yield return new WaitForSeconds(_nextNodeDelay);
            stopReading = false;
            ReadSingleLine();
        }

        #endregion
    }
}
