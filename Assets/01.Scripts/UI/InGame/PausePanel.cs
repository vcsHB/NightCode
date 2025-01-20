using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame.SystemUI
{

    public class PausePanel : UIPanel
    {
        //[SerializeField] private 
        private RectTransform _rectTrm;
        [SerializeField] private Image _topLine;
        [SerializeField] private Image _bottomLine;
        private int _lineUnscaledTimeHash = Shader.PropertyToID("_CurrentUnscaledTime");

        protected override void Awake()
        {
            base.Awake();
            _rectTrm = transform as RectTransform;
        }
        public override void Open()
        {
            base.Open();
            _isActive = true;
            _rectTrm.DOScaleY(1f, _activeDuration).SetUpdate(_useUnscaledTime);
        }

        public override void Close()
        {
            base.Close();
        }


        private void Update()
        {
            if (_isActive)
            {
                _topLine.material.SetFloat(_lineUnscaledTimeHash, Time.unscaledTime);
                _bottomLine.material.SetFloat(_lineUnscaledTimeHash, Time.unscaledTime);
            }
        }

    }

}