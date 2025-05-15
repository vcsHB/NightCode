using Core.StageController;
using DG.Tweening;
using UnityEngine;

namespace Tutorial
{
    public class CheckSkipStagePanel : MonoBehaviour
    {
        public StageSO stageToSkip;
        private CanvasGroup _canvasGroup;
        private Tween _toggleTween;
        private float _duration = 0.3f;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (stageToSkip != null) Open();
        }

        public void Open()
        {
            if (_toggleTween != null && _toggleTween.active) _toggleTween.Kill();
            _toggleTween = _canvasGroup.DOFade(1, _duration).SetUpdate(true);

            Time.timeScale = 0;
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

        public void SkipTutorial()
        {
            Time.timeScale = 1;
            StageManager.Instance.currentStage = stageToSkip;
            StageManager.Instance.LoadScene();
        }
    }
}
