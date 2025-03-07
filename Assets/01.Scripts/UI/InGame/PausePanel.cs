using DG.Tweening;
using InputManage;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.InGame.SystemUI
{

    public class PausePanel : UIPanel
    {
        [SerializeField] private UIInputReader _uiInput;
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
            Time.timeScale = 0f;
            _buttonGroup.Open();
            _rectTrm.DOSizeDelta(new Vector2(_rectTrm.sizeDelta.x, _activeHeight), _activeDuration).SetUpdate(_useUnscaledTime);
            SetTweenLinesFillAmount(1f);
        }

        public override void Close()
        {
            base.Close();
            Time.timeScale = 1f;
            _buttonGroup.Close();
            _rectTrm.DOSizeDelta(new Vector2(_rectTrm.sizeDelta.x, 0f), _activeDuration).SetUpdate(_useUnscaledTime);
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
            if (Keyboard.current.nKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("SpeedRunScene");
            }
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