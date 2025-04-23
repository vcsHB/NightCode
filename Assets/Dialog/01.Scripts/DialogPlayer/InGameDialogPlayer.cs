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
        [SerializeField] private List<Actor> _characters;

        private Actor _curCharacter;
        private TMP_TextInfo _txtInfo;
        private bool _optionSelected = false;
        private NodeSO _nextNode;
        private List<OptionButton> _optionBtns;

        protected override void Awake()
        {
            base.Awake();
            _animPlayer = GetComponent<AnimationPlayer>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartDialog();
            }
        }

        private void LateUpdate()
        {
            if (_curReadingNode is NormalNodeSO node && _isReadingDialog)
            {
                _animPlayer.PlayAnimation(_curCharacter.ContentText, node.contentTagAnimations);
            }
        }


        #region DialogRead

        public override void StartDialog()
        {
            if (_isReadingDialog)
            {
                Debug.LogWarning("A Dialog is already running in this player\nYou can not run multiple dialog in single player");
                return;
            }

            _isReadingDialog = true;
            _curReadingNode = dialog.nodes[0];
            ReadSingleLine();
        }

        public override void EndDialog()
        {
            _characters.ForEach((c) => RemoveTalkbubble(c.personalTalkBubble));
            _isReadingDialog = false;
            _curReadingNode = null;
            OnDialogueEnd?.Invoke();
        }

        public override void ReadSingleLine()
        {
            if (_curReadingNode == null)
            {
                EndDialog();
                return;
            }

            StartCoroutine(ReadingNodeRoutine());
            DialogConditionManager.Instance.CountVisit(_curReadingNode.guid);
        }

        #endregion


        #region ReadingRoutines

        private IEnumerator ReadingNodeRoutine()
        {
            _isReadingDialog = false;
            _curReadingNode.dialogEvents.ForEach(dialogEvent => dialogEvent.PlayEvent());

            yield return new WaitUntil(() => !_curReadingNode.dialogEvents.Exists(dialogEvent => dialogEvent.isCompleteEvent == false));
           
            _isReadingDialog = true;
            if (_curReadingNode is NormalNodeSO normal)
            {
                _characters.ForEach(c =>
                {
                    if (c.name == normal.GetReaderName())
                    {
                        _curCharacter = c;
                        _curCharacter.personalTalkBubble.SetEnabled();
                    }
                });
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
            TextMeshProUGUI tmp = _curCharacter.ContentText;

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
            StartCoroutine(WaitNodeRoutine(
                () => GetInput(),
                () => _curCharacter.personalTalkBubble.SetDisabled()));
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

        private IEnumerator WaitNodeRoutine(Func<bool> waitPredict, Action endAction)
        {
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(waitPredict);

            CompleteNodeAnim(_curReadingNode);
            _playingEndAnimation = true;
            yield return new WaitUntil(() => !_playingEndAnimation);

            endAction?.Invoke();
            _curReadingNode = _nextNode;
            _isReadingDialog = false;

            yield return new WaitForSeconds(_nextNodeDelay);
            ReadSingleLine();
        }

        private void JudgementCondition(BranchNodeSO branch)
        {
            bool decision = branch.condition.Decision();
            _curReadingNode = branch.nextNodes[decision ? 0 : 1];
            ReadSingleLine();
        }

        #endregion

        private void InitNodeAnim(NodeSO node)
        {
            List<TagAnimation> anims = node.GetAllAnimations();

            anims.ForEach((anim) =>
            {
                anim.Init();

                if (anim is SpriteAnimation srAnim)
                    srAnim.Init(_curCharacter.spriteRenderer);

                if (anim is StopReadingAnimation stopAnim)
                    stopAnim.Init(this);
            });
        }

        private void CompleteNodeAnim(NodeSO node)
        {
            List<TagAnimation> anims = node.GetAllAnimations();
            anims.ForEach((anim) => anim.Complete());
        }
        public virtual void SetCharacters(List<Actor> actors)
        {
            this._characters = actors;

            foreach (Actor actor in _characters)
            {
                TalkBubble bubble = GetTalkBubble();
                bubble.SetDisabled();
                actor.personalTalkBubble = bubble;

                switch (actor.actorType)
                {
                    case ActorType.Player:
                        bubble.SetOwner(CafePlayerManager.Instance.playerTrm, actor.bubbleOffset);
                        break;
                    case ActorType.Object:
                        bubble.SetOwner(actor.target, actor.bubbleOffset);
                        break;
                }
            }
        }
    }
    public enum ActorType
    {
        Player,
        Object
    }

    [Serializable]
    public class Actor
    {
        public string name;
        public Vector2 bubbleOffset;
        public ActorType actorType;
        public Transform target; // owner character of this conversation
        public SpriteRenderer spriteRenderer;
        public TalkBubble personalTalkBubble;

        public TextMeshProUGUI ContentText => personalTalkBubble.ContentTextMeshPro;
    }
}
