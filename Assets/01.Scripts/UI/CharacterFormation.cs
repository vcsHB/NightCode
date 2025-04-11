using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Office
{
    public class CharacterFormation : OfficeUIParent
    {
        public List<RectTransform> formationTrm;
        public List<CharacterFormationSlot> slots;

        [SerializeField] private MissionSelectPanel _missionSelectPanel;
        [SerializeField] private float _openCloseDuration;
        [SerializeField] private float _formationSetDuration;
        private Tween _openCloseTween;
        private Tween _changeTween;
        private Sequence _slotPositionSetSeq;

        private Vector2 screenPosition = new Vector2(Screen.width, Screen.height);

        protected override void Awake()
        {
            base.Awake();
            RectTrm.anchoredPosition = new Vector2(RectTrm.anchoredPosition.x, -screenPosition.y);
            for (int i = 0; i < 3; i++)
            {
                slots[i].Init(this);
                slots[i].index = i;
            }
        }

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


        public override void OpenAnimation()
        {
            if (_openCloseTween != null && _openCloseTween.active) _openCloseTween.Kill();

            if (_changeTween != null && _changeTween.active) _changeTween.Complete();
            if (_slotPositionSetSeq != null && _slotPositionSetSeq.active) _slotPositionSetSeq.Complete();


            _openCloseTween = RectTrm.DOAnchorPosY(0f, _openCloseDuration)
                .OnComplete(OnCompleteOpen);
        }

        public override void CloseAnimation()
        {
            if (_openCloseTween != null && _openCloseTween.active) _openCloseTween.Kill();

            if (_changeTween != null && _changeTween.active) _changeTween.Complete();
            if (_slotPositionSetSeq != null && _slotPositionSetSeq.active) _slotPositionSetSeq.Complete();


            _openCloseTween = RectTrm.DOAnchorPosY(-screenPosition.y, _openCloseDuration)
                .OnComplete(OnCompleteClose);
        }
    }
}
