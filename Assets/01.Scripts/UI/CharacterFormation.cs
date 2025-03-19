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

        [SerializeField] private float _duration;
        private Tween _changeTween;
        private Sequence _slotPositionSetSeq;

        private void Awake()
        {
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
            _changeTween = slot.RectTransform.DOAnchorPosX(formationTrm[changingIndex].anchoredPosition.x, _duration)
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
                    .DOAnchorPos(formationTrm[i].anchoredPosition, _duration));
            }
        }


        public override void OpenAnimation()
        {

        }

        public override void CloseAnimation()
        {

        }
    }
}
