using Base;
using Core.StageController;
using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Office
{
    public class CharacterFormation : OfficeUIParent
    {
        public List<RectTransform> formationTrm;
        public List<CharacterFormationSlot> slots;

        //[SerializeField] private MissionSelectPanel _missionSelectPanel;
        [SerializeField] private BaseInput _baseInput;
        [SerializeField] private float _toggleDuration;
        [SerializeField] private float _formationSetDuration;
        [SerializeField] private RectTransform _panelRect;
        [SerializeField] private Button _nextStageButton;
        [Space]
        [SerializeField] private TextMeshProUGUI _stageText;
        [SerializeField] private Image _stageIcon;

        private bool _isIngame = false;
        private CanvasGroup _canvasGruop;
        private Sequence _toggleSequence;
        private Tween _changeTween;
        private Sequence _slotPositionSetSeq;

        private Vector2 screenPosition = new Vector2(Screen.width, Screen.height);

        protected override void Awake()
        {
            base.Awake();
            _canvasGruop = GetComponent<CanvasGroup>();
            // RectTrm.anchoredPosition = new Vector2(RectTrm.anchoredPosition.x, -screenPosition.y);
            for (int i = 0; i < 3; i++)
            {
                slots[i].Init(this);
                slots[i].index = i;
            }

            _nextStageButton.onClick.AddListener(() => StageManager.Instance.LoadNextStage());
        }


        #region Slot

        public CharacterEnum GetCharacter(int index)
            => slots[index].characterType;

        public void OnDrag(CharacterFormationSlot characterFormationSlot)
        {
            for (int i = 0; i < 3; i++)
            {
                int index = characterFormationSlot.index;
                float slotPosition = characterFormationSlot.RectTransform.anchoredPosition.x;

                if (i == index) continue;

                // 오른쪽에 있는 애보다 더 오른쪽에 있다면 바꿔
                if (i > index && slotPosition > formationTrm[i].anchoredPosition.x)
                {
                    if (_changeTween != null && _changeTween.active) return;
                    ChangeSlot(i, index);
                }

                //왼쪽에 있는 애보다 더 왼쪽에 있다면 바꿔
                if (i < index && slotPosition < formationTrm[i].anchoredPosition.x)
                {
                    if (_changeTween != null && _changeTween.active) return;
                    ChangeSlot(i, index);
                }
            }
        }

        public void ChangeSlot(int toChangeIndex, int changingIndex)
        {
            if (_changeTween != null && _changeTween.active)
                _changeTween.Complete();

            if (_slotPositionSetSeq != null && _slotPositionSetSeq.active)
                _slotPositionSetSeq.Complete();

            CharacterFormationSlot slot = slots[toChangeIndex];
            _changeTween = slot.RectTransform.DOAnchorPosX(formationTrm[changingIndex].anchoredPosition.x, _formationSetDuration)
                .OnComplete(() =>
                {
                    slots[changingIndex].index = toChangeIndex;
                    slots[toChangeIndex].index = changingIndex;

                    CharacterFormationSlot temp = slots[toChangeIndex];
                    slots[toChangeIndex] = slots[changingIndex];
                    slots[changingIndex] = temp;
                });
        }

        public void SetFormationSlotPosition()
        {
            if (_changeTween != null && _changeTween.active)
                _changeTween.Complete();

            if (_slotPositionSetSeq != null && _slotPositionSetSeq.active)
                _slotPositionSetSeq.Complete();

            _slotPositionSetSeq = DOTween.Sequence();

            for (int i = 0; i < 3; i++)
            {
                _slotPositionSetSeq.Join(slots[i].RectTransform
                    .DOAnchorPos(formationTrm[i].anchoredPosition, _formationSetDuration));
            }
        }

        #endregion


        public override void OpenAnimation()
        {
            if (_toggleSequence != null && _toggleSequence.active) _toggleSequence.Kill();

            if (_changeTween != null && _changeTween.active) _changeTween.Complete();
            if (_slotPositionSetSeq != null && _slotPositionSetSeq.active) _slotPositionSetSeq.Complete();


            _baseInput.DisableInput();
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _toggleSequence = DOTween.Sequence();

            _toggleSequence.Append(_panelRect.DOAnchorPosY(0f, _toggleDuration))
                .Join(_canvasGruop.DOFade(0.85f, _toggleDuration))
                .OnComplete(OnCompleteOpen);
        }

        public override void CloseAnimation()
        {
            if (_toggleSequence != null && _toggleSequence.active) _toggleSequence.Kill();

            if (_changeTween != null && _changeTween.active) _changeTween.Complete();
            if (_slotPositionSetSeq != null && _slotPositionSetSeq.active) _slotPositionSetSeq.Complete();

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _toggleSequence = DOTween.Sequence();

            _toggleSequence.Append(_panelRect.DOAnchorPosY(-screenPosition.y, _toggleDuration))
                .Join(_canvasGruop.DOFade(0f, _toggleDuration))
                .OnComplete(OnCompleteClose);
        }

        protected override void OnCompleteClose()
        {
            base.OnCompleteClose();
            _baseInput.EnableInput();
        }

        public void Init(StageSO stageSO)
        {
            _isIngame = stageSO is IngameSO;
            _stageText.SetText(stageSO.displayStageName);
            _stageIcon.sprite = stageSO.stageIcon;

            if (_isIngame == false)
            {
                slots.ForEach(slot =>
                {
                    if (slot.characterType == CharacterEnum.Cross)
                    {
                        slot.RectTransform.anchoredPosition = formationTrm[1].anchoredPosition;
                        slot.enabled = false;
                    }
                    else
                    {
                        slot.gameObject.SetActive(false);
                    }
                });
            }
            else
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    var slot = slots[i];

                    slot.enabled = true;
                    slot.gameObject.SetActive(true);
                    slot.RectTransform.anchoredPosition = formationTrm[i].anchoredPosition;
                }

                slots.ForEach(slot =>
                {
                });
            }
        }
    }
}
