using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.InGame.SystemUI
{

    public class PausePanel : UIPanel, IWindowPanel
    {
        [SerializeField] private PauseButtonGroup _buttonGroup;
        [SerializeField] private RectTransform _panelRectTrm;
        private RectTransform _rectTrm;
        [SerializeField] private float _activeHeight;
        [SerializeField] private Image _topLine;
        [SerializeField] private Image _bottomLine;
        private int _lineUnscaledTimeHash = Shader.PropertyToID("_CurrentUnscaledTime");

        protected override void Awake()
        {
            base.Awake();
            _rectTrm = transform as RectTransform;

        }



        private void OnDestroy()
        {

        }
        public override void Open()
        {
            base.Open();
            _isActive = true;
            Time.timeScale = 0f;
            _buttonGroup.Open();
            _rectTrm.DOSizeDelta(new Vector2(_rectTrm.sizeDelta.x, _activeHeight), _activeDuration).SetUpdate(_useUnscaledTime);
            SetTweenLinesFillAmount(1f);
        }

        public override void Close()
        {
            base.Close();
            _isActive = false;
            Time.timeScale = 1f;
            _buttonGroup.Close();
            _rectTrm.DOSizeDelta(new Vector2(_rectTrm.sizeDelta.x, 0f), _activeDuration).SetUpdate(_useUnscaledTime);
            SetTweenLinesFillAmount(0f);
        }

        public void CloseVisual()
        {
            SetCanvasActive(false);
            _isActive = false;

            _buttonGroup.Close();
            _rectTrm.DOSizeDelta(new Vector2(_rectTrm.sizeDelta.x, 0f), _activeDuration).SetUpdate(_useUnscaledTime);
            SetTweenLinesFillAmount(0f);
            OnCloseEvent?.Invoke();
        }

        public void Toggle()
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

        public void HandleMoveExit()
        {
            SceneManager.LoadScene("BasementScene_V2");
        }

    }

}