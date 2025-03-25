using Basement;
using Basement.Training;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Office
{
    public class CharacterPanel : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CharacterEnum _characterType;
        [SerializeField]private Image _disableMask;

        private CharacterSelectPanel _selectPanel;
        private int _index;
        private Sequence _enableDisableSeq;
        private float _duration = 0.2f;

        public RectTransform RectTrm => transform as RectTransform;
        public CharacterEnum CharacterType => _characterType;

        private void Awake()
        {
            _selectPanel = GetComponentInParent<CharacterSelectPanel>();
        }

        public void SetIndex(int i)
        {
            _index = i;
        }


        public void EnablePanel()
        {
            if (_enableDisableSeq != null && _enableDisableSeq.active) _enableDisableSeq.Kill();

            _enableDisableSeq = DOTween.Sequence();

            _enableDisableSeq.Append(_disableMask.DOFade(0f, _duration))
                .Join(transform.DOScale(1f, _duration));
        }

        
        public void DisablePanel()
        {
            if (_enableDisableSeq != null && _enableDisableSeq.active) _enableDisableSeq.Kill();

            _enableDisableSeq = DOTween.Sequence();

            _enableDisableSeq.Append(_disableMask.DOFade(0.85f, _duration))
                .Join(transform.DOScale(0.9f, _duration));
        }


        #region MouseEvent

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_index == 1) return;
            RectTrm.localScale = Vector3.one * 1f;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_index == 1) return;
            RectTrm.localScale = Vector3.one * 1.02f;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SelectCharacter();
        }

        private void SelectCharacter()
        {
            _selectPanel.SelectCharacter(_index);
        }

        #endregion
    }
}
