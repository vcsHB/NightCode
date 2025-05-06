using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dialog
{
    public class DialogOption : MonoBehaviour
    {
        [SerializeField] private OptionNodeSO DebugOption;

        private List<DialogOptionObject> _optionList = new();
        private bool _isCheckingOption;

        private Tween _toggleTween;
        private float _duration = 0.2f;
        private int _selectedIndex = -1;

        private RectTransform rectTrm => transform as RectTransform;

        private void Awake()
        {
            _optionList = GetComponentsInChildren<DialogOptionObject>().ToList();
            //SetOption(DebugOption, null);
        }

        private void Update()
        {
            if (_isCheckingOption == false) return;

            if (Mouse.current.position.value.x < Screen.width / 2)
            {
                CheckOption(0);
                _optionList[0].OnHover(true);
                _optionList[1].OnHover(false);
            }
            else
            {
                CheckOption(1);
                _optionList[0].OnHover(false);
                _optionList[1].OnHover(true);
            }
        }

        private void CheckOption(int index)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _selectedIndex = index;

                _optionList[index == 0 ? 0 : 1].OnSelectOption();
                _optionList[index == 0 ? 1 : 0].Close();
                _isCheckingOption = false;
            }
        }

        public void SetOption(OptionNodeSO option, Action<Option> onComplete)
        {
            _selectedIndex = -1;
            _isCheckingOption = true;

            for (int i = 0; i < 2; i++)
            {
                int index = i;
                _optionList[index].Open();
                _optionList[index].SetOption(option.options[index]);
                _optionList[index].onSelect = () => onComplete?.Invoke(option.options[index]);
            }

            if (_toggleTween != null && _toggleTween.active)
                _toggleTween.Kill();

            _toggleTween = rectTrm.DOAnchorPosY(30f, _duration);
        }

        public void Close()
        {
            if (_toggleTween != null && _toggleTween.active)
                _toggleTween.Kill();

            _toggleTween = rectTrm.DOAnchorPosY(-rectTrm.rect.height, _duration);
        }
    }
}
