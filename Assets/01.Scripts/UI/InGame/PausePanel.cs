using DG.Tweening;
using InputManage;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame.SystemUI
{

    public class PausePanel : UIPanel
    {
        [SerializeField] private UIInputReader _uiInput;
        [SerializeField] private RectTransform _panelRectTrm;
        [SerializeField] private Image _topLine;
        [SerializeField] private Image _bottomLine;
        private int _lineUnscaledTimeHash = Shader.PropertyToID("_CurrentUnscaledTime");

        protected override void Awake()
        {
            base.Awake();
            _uiInput.OnEscEvent += HandleTogglePausePanel;

        }

        private void OnDestroy()
        {
            _uiInput.OnEscEvent -= HandleTogglePausePanel;

        }
        public override void Open()
        {
            base.Open();
            _isActive = true;
            _panelRectTrm.DOScaleY(1f, _activeDuration).SetUpdate(_useUnscaledTime);
            SetTweenLinesFillAmount(1f);
        }

        public override void Close()
        {
            base.Close();
            _panelRectTrm.DOScaleY(0f, _activeDuration).SetUpdate(_useUnscaledTime);
            SetTweenLinesFillAmount(0f);
        }

        private void HandleTogglePausePanel()
        {
            if (_isActive)
                Close();
            else
                Open();
        }


        private void Update()
        {
            if (_isActive)
            {
                _topLine.material.SetFloat(_lineUnscaledTimeHash, Time.unscaledTime);
                _bottomLine.material.SetFloat(_lineUnscaledTimeHash, Time.unscaledTime);
            }
        }

        private void SetTweenLinesFillAmount(float value)
        {
            _topLine.DOFillAmount(value, _activeDuration).SetUpdate(_useUnscaledTime);
            _bottomLine.DOFillAmount(value, _activeDuration).SetUpdate(_useUnscaledTime);
        }

    }

}