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


        private void Awake()
        {
            _optionList = GetComponentsInChildren<DialogOptionObject>().ToList();
            //SetOption(DebugOption, null);
        }

        private void Update()
        {
            if (_isCheckingOption)
            {
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
        }

        private void CheckOption(int index)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _optionList[index].OnSelectOption();
            }
        }

        public void SetOption(OptionNodeSO option, Action<NodeSO> onComplete)
        {
            _isCheckingOption = true;

            for (int i = 0; i < option.options.Count; i++)
            {
                int index = i;
                _optionList[index].SetOption(option.options[index]);
                _optionList[index].onSelect = () => onComplete?.Invoke(option.options[index].nextNode);
            }
        }
    }
}
