using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Tutorial
{

    public class DescriptionPanel : UIPanel
    {
        [SerializeField] private Image _topLineImage;
        [SerializeField] private Image _bottomeLineImage;
        private int _lineUnscaledTimeHash = Shader.PropertyToID("_CurrentUnscaledTime");
        [SerializeField] private Image _waitGauge;
        [SerializeField] private Button _closeButton;
        [SerializeField] private float _closeWaitDuration = 3f;


        protected override void Awake()
        {
            base.Awake();
            _closeButton.onClick.AddListener(Close);

        }
        public override void Open()
        {
            base.Open();
            _closeButton.interactable = false;
            _waitGauge.fillAmount = 0f;
            _waitGauge.DOFillAmount(1f, _closeWaitDuration).SetUpdate(true).OnComplete(() => _closeButton.interactable = true);
            Time.timeScale = 0f;
        }

        public override void Close()
        {
            base.Close();
            Time.timeScale = 1f;
        }
        private void Update()
        {

            if (_isActive)
            {
                _topLineImage.material.SetFloat(_lineUnscaledTimeHash, Time.unscaledTime);
                _bottomeLineImage.material.SetFloat(_lineUnscaledTimeHash, Time.unscaledTime);
            }
        }


    }

}