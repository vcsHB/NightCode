using SoundManage;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using static Unity.Burst.Intrinsics.X86.Avx;

namespace Dialog
{
    [RequireComponent(typeof(AnimationPlayer))]
    public class InGameDialogPlayer : DialogPlayer
    {
        protected AnimationPlayer _animPlayer;
        [SerializeField] protected DialogOption _option;

        protected Actor _currentActor;
        protected Action _waitCompleteAction;
        protected OptionNodeSO _optionTalk;
        protected NodeSO _nextNode;

        private bool _isReadingNodeComplete = false;

        protected override void Awake()
        {
            base.Awake();
            _animPlayer = GetComponent<AnimationPlayer>();
        }

        private void Update()
        {
            if (_isReadingDialog == false) return;

            if(_isReadingNodeComplete == false && GetInput())
            {
                StopCoroutine(_readingNodeRoutine);
                _currentActor.ContentText.maxVisibleCharacters = _currentActor.ContentText.text.Length;

                _isReadingNodeComplete = true;
                _nextNode = (_curReadingNode as NormalNodeSO).nextNode;
                _waitCompleteAction = _currentActor.OnCompleteNode;
                StartCoroutine(WaitNodeRoutine(GetInput));
            }
        }

        public override void SkipDialog()
        {
            
            base.SkipDialog();

        }
        #region Animation

        protected void LateUpdate()
        {
            if (_curReadingNode is NormalNodeSO node && _isReadingDialog)
            {
                _animPlayer.PlayAnimation(_currentActor.ContentText, node.contentTagAnimations);
            }
        }

        protected void InitNodeAnim(NodeSO node)
        {
            List<TagAnimation> anims = node.GetAllAnimations();

            anims.ForEach((anim) =>
            {
                anim.Init();

                if (anim is SpriteAnimation srAnim) srAnim.Init(_currentActor.spriteRenderer);
                if (anim is StopReadingAnimation stopAnim) stopAnim.Init(this);
            });
        }

        protected void CompleteNodeAnim(NodeSO node)
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
                DialogActorManager.TryGetActor(node.GetReaderName(), out _currentActor);
            }

            _curReadingNode.startDialogEvent.ForEach(dialogEvent => dialogEvent.PlayEvent(this, _currentActor));
            yield return new WaitUntil(() => !_curReadingNode.startDialogEvent.Exists(dialogEvent => dialogEvent.isCompleteEvent == false));

            _isReadingDialog = true;
            if (_curReadingNode is NormalNodeSO normal)
            {
                _currentActor?.personalTalkBubble.SetEnabled();
                _readingNodeRoutine = StartCoroutine(ReadingNormalNodeRoutine(normal));
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

        protected virtual IEnumerator ReadingNormalNodeRoutine(NormalNodeSO node)
        {
            TextMeshProUGUI tmp = _currentActor.ContentText;

            tmp.SetText(node.GetContents());
            tmp.maxVisibleCharacters = 0;
            InitNodeAnim(node);
            _isReadingDialog = true;
            _isReadingNodeComplete = false;
            while (tmp.maxVisibleCharacters < tmp.text.Length)
            {
                if (tmp.text[tmp.maxVisibleCharacters++] == ' ') continue;

                if (node.textOutSound != null)
                    SoundController.Instance.PlaySound(node.textOutSound, _currentActor.target.position);

                yield return new WaitForSeconds(_textOutDelay);
                yield return new WaitUntil(() => stopReading == false);
            }
            _nextNode = node.nextNode;
            _isReadingNodeComplete = true;
            _waitCompleteAction = _currentActor.OnCompleteNode;
            StartCoroutine(WaitNodeRoutine(GetInput));
            
        }

        protected virtual void ReadingOptionNodeRoutine(OptionNodeSO node)
        {
            InitNodeAnim(node);
            _option.SetOption(node, OnSelectOption);
            //StartCoroutine(WaitNodeRoutine(() => _optionSelected, null));
        }

        protected virtual void OnSelectOption(Option option)
        {
            _playingEndAnimation = false;

            _optionTalk = _curReadingNode as OptionNodeSO;
            _curReadingNode = ScriptableObject.CreateInstance<NormalNodeSO>();
            (_curReadingNode as NormalNodeSO).SetNormalNodeByOption(option);
            ReadSingleLine();
        }

        protected virtual void JudgementCondition(BranchNodeSO branch)
        {
            bool decision = branch.condition.Decision();
            _curReadingNode = branch.nextNodes[decision ? 0 : 1];
            _playingEndAnimation = false;
            ReadSingleLine();
        }

        protected virtual IEnumerator WaitNodeRoutine(Func<bool> waitPredict)
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

            _waitCompleteAction?.Invoke();
            _curReadingNode = _nextNode;
            _isReadingDialog = false;

            if (_optionTalk)
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
