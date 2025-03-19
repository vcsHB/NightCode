using Basement;
using Basement.Training;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Office
{
    public class CharacterPanel : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public bool isSelected = false;
        [SerializeField] private CharacterEnum _characterType;
        [SerializeField] private TextMeshProUGUI _strText;
        [SerializeField] private TextMeshProUGUI _intText;
        [SerializeField] private TextMeshProUGUI _dexText;

        private CharacterSelectPanel _selectPanel;
        private int _index;

        public RectTransform RectTrm => transform as RectTransform;
        public CharacterEnum CharacterType => _characterType;

        private void Awake()
        {
            _selectPanel = GetComponentInParent<CharacterSelectPanel>();
        }

        public void Init(int i)
        {
            _index = i;
        }

        public void UpdateStat()
        {
            //int str = CharacterManager.Instance.GetSkillPoint(_characterType, SkillPointEnum.Health);
            //int intel = CharacterManager.Instance.GetSkillPoint(_characterType, SkillPointEnum.Intelligence);
            //int dex = CharacterManager.Instance.GetSkillPoint(_characterType, SkillPointEnum.Dexdexterity);

            //_strText.SetText($"str pt: {str}");
            //_intText.SetText($"int pt: {intel}");
            //_dexText.SetText($"dex pt: {dex}");
        }


        #region MouseEvent

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isSelected) return;
            RectTrm.localScale = Vector3.one * 1f;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isSelected) return;
            RectTrm.localScale = Vector3.one * 1.02f;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SelectCharacter();
        }

        private void SelectCharacter()
        {
            isSelected = true;
            _selectPanel.SelectCharacter(_index);
        }

        #endregion
    }
}
