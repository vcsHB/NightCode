using DG.Tweening;
using StatSystem;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Office.CharacterSkillTree
{
    public class CharacterStatIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _characterName;

        [SerializeField] private RectTransform _indicatorBG;
        [SerializeField] private RectTransform _slotParent;
        [SerializeField] private StatSlot _statSlotPf;
        [SerializeField] private Button _openCloseButton;
        [SerializeField] private float _easingDuration;

        [SerializeField] private List<string> _characterNameList;
        private bool _isOpen = false;
        private Tween _openCloseTween;
        private readonly float _closePos = 250, _openPos = -250;

        public void SetCharacter(CharacterEnum characterType)
        {
            _slotParent.GetComponentsInChildren<StatSlot>().ToList()
                .ForEach(prevSlot => Destroy(prevSlot.gameObject));


            _characterName.SetText(_characterNameList[(int)characterType]);
            StatGroupSO statGroup = CharacterStatManager.Instance.statGroup[characterType];

            statGroup.statList.ForEach(stat =>
            {
                StatSlot slot = Instantiate(_statSlotPf, _slotParent);
                slot.SetStat(stat);
            });
        }

        public void InteractButton()
        {
            _isOpen = !_isOpen;
            _openCloseButton.transform.Rotate(new Vector3(0, 0, 180));

            if (_openCloseTween != null && _openCloseTween.active) _openCloseTween.Kill();
            _openCloseTween = _indicatorBG.DOAnchorPosX(_isOpen ? _openPos : _closePos, _easingDuration);
        }
    }
}
