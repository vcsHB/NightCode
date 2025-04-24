using System;
using System.Collections;
using System.Collections.Generic;
using Cafe;
using Combat.PlayerTagSystem;
using TMPro;
using UI.InGame.GameUI.CallTalk;
using UI.InGame.SystemUI;
using UnityEngine;

namespace Dialog
{
    [RequireComponent(typeof(AnimationPlayer))]
    public class InGameDialogPlayer : DialogPlayer
    {
        private AnimationPlayer _animPlayer;

        [SerializeField] private RectTransform _optionParent;

        private Actor _currentActor;
        private bool _optionSelected = false;
        private NodeSO _nextNode;
        private List<OptionButton> _optionBtns;

        protected override void Awake()
        {
            base.Awake();
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
            _optionSelected = false;
            _optionBtns = new List<OptionButton>();
            _optionParent.gameObject.SetActive(true);
            InitNodeAnim(node);

            for (int i = 0; i < node.options.Count; i++)
            {
                OptionButton optionButton = Instantiate(node.optionPf, _optionParent);
                optionButton.SetOption(node.options[i], _animPlayer);
                optionButton.OnClcickEvent += OnSelectOption;

                _optionBtns.Add(optionButton);
            }

            StartCoroutine(WaitNodeRoutine(
                () => _optionSelected,
                () =>
                {
                    _optionParent.gameObject.SetActive(false);
                    _optionBtns.ForEach(option => Destroy(option.gameObject));
                    _optionBtns.Clear();
                }));
        }

        private void OnSelectOption(NodeSO node)
        {
            _optionSelected = true;
            _nextNode = node;
        }

        private void JudgementCondition(BranchNodeSO branch)
        {
            bool decision = branch.condition.Decision();
            _curReadingNode = branch.nextNodes[decision ? 0 : 1];
            ReadSingleLine();
        }

        private IEnumerator WaitNodeRoutine(Func<bool> waitPredict, Action endAction)
        {
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(waitPredict);

            CompleteNodeAnim(_curReadingNode);
            _playingEndAnimation = true;
            yield return new WaitUntil(() => !_playingEndAnimation);

            _curReadingNode.endDialogEvent.ForEach(dialogEvent => dialogEvent.PlayEvent(this, _currentActor));
            yield return new WaitUntil(() => !_curReadingNode.endDialogEvent.Exists(dialogEvent => dialogEvent.isCompleteEvent == false));

            endAction?.Invoke();
            _curReadingNode = _nextNode;
            _isReadingDialog = false;

            yield return new WaitForSeconds(_nextNodeDelay);
            ReadSingleLine();
        }

        #endregion
    }
}
