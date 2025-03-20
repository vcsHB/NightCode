using CameraControllers;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    public class ProcessInputObject : CafePlayerInputObject
    {
        public event Action onComplete;

        private CafeTable _table;
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

        public void Init(CafeTable table)
        {
            _table = table;
            Open();
        }

        private void OnLeftClick()
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
            CameraManager.Instance.GetCompo<CameraShakeController>().Shake(4, 0.03f);


            if (_process > _targetProcess)
            {
                onComplete?.Invoke();
                Close();
            }
        }


        public override void Open()
        {
            gameObject.SetActive(true);
            _wasClickedLeft = false;
            _process = 0; 
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
