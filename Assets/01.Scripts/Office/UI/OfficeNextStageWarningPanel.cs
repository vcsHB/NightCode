using Core.StageController;
using DG.Tweening;
using Office;
using UI;
using UnityEngine;

namespace Base.Office
{
    public class OfficeNextStageWarningPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private CharacterFormation _characterFormation;

        private CanvasGroup _canvasGroup;
        private Tween _toggleTween;
        private float _duration = 0.3f;


        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Open()
        {
            if(_toggleTween != null && _toggleTween.active) _toggleTween.Kill();
            _toggleTween = _canvasGroup.DOFade(1, _duration).SetUpdate(true);

            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Close()
        {
            Time.timeScale = 1;
            if (_toggleTween != null && _toggleTween.active) _toggleTween.Kill();
            _toggleTween = _canvasGroup.DOFade(0, _duration);

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
        public void Accept()
        {
            Close();
            _characterFormation.Init(StageManager.Instance.GetNextStage());
            _characterFormation.Open();
        }
    }
}