using CameraControllers;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    public class ProcessInputObject : CafePlayerInputObject
    {
        public Action OnComplete;
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private float _processPerClick;
        [SerializeField] private Image _leftClickBtn, _rightClickBtn;
        [SerializeField] private Color _enableColor, _disableColor;

        private bool _wasClickedLeft = false;
        private float _process = 0;
        private float _targetProcess = 2;



        private void OnEnable()
        {
            input.onLeftClick += OnLeftClick; 
            input.onRightClick += OnRightClick;
        }

        private void OnDisable()
        {
            input.onLeftClick -= OnLeftClick;
            input.onRightClick -= OnRightClick;
        }

        public void Init()
        {
            Open();
        }

        private void OnLeftClick(bool isPress)
        {
            if (_wasClickedLeft) return;

            _wasClickedLeft = true;
            _leftClickBtn.color = _disableColor;
            _rightClickBtn.color = _enableColor;

            AddProcess();
        }

        private void OnRightClick()
        {
            if (_wasClickedLeft == false) return;

            _wasClickedLeft = false;
            _leftClickBtn.color = _enableColor;  
            _rightClickBtn.color = _disableColor;

            AddProcess();
        }

        private void AddProcess()
        {
            _process += _processPerClick;
            _frame.fillAmount = _process / _targetProcess;
            
            if (_process >= _targetProcess)
            {
                _process = 0; OnComplete?.Invoke();
                Close();
            }
        }


        public override void Open()
        {
            gameObject.SetActive(true);
            _wasClickedLeft = false;
            _frame.fillAmount = 0;
            _process = 0; 
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
