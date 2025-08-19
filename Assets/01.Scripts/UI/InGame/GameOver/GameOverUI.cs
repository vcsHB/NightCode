using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame.SystemUI
{
    public class GameOverUI : MonoBehaviour, IWindowPanel
    {
        [SerializeField]
        private RectTransform _topPanelGroupTrm;
        [SerializeField]
        private RectTransform _bottomPanelGroupTrm;
        [SerializeField] private float _topDefaultPos;
        [SerializeField] private float _topActivePos;
        [SerializeField] private float _bottomDefaultPos;
        [SerializeField] private float _bottomActivePos;
        [SerializeField] private float _edgePanelDuration;
        [SerializeField] private float _goToSelectPanelDuration;
        [Header("Panel Layer Setting")]
        [SerializeField] private float _layer1Duration;
        [SerializeField] private float _layer2Duration;

        [SerializeField] private Image _topLayer1;
        [SerializeField] private Image _topLayer2;
        [SerializeField] private Image _bottomLayer1;
        [SerializeField] private Image _bottomLayer2;
        [SerializeField] private bool _useUnscaledTime;
        [SerializeField] private CanvasGroup _goToSelectScenePanel;
        private CanvasGroup _canvasGroup;

        void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        [ContextMenu("Open")]
        public void Open()
        {
            _canvasGroup.DOFade(1f, 0.2f).SetUpdate(_useUnscaledTime);
            Sequence seq = DOTween.Sequence();
            seq.SetUpdate(_useUnscaledTime);
            seq.AppendCallback(() =>
            {
                _topPanelGroupTrm.DOAnchorPosY(_topActivePos, _edgePanelDuration);
                _bottomPanelGroupTrm.DOAnchorPosY(_bottomActivePos, _edgePanelDuration);

            });
            seq.JoinCallback(() =>
            {
                _topLayer1.DOFillAmount(0.8f, _layer1Duration);
                _bottomLayer1.DOFillAmount(0.8f, _layer1Duration);
            });
            seq.AppendCallback(() =>
            {
                _topLayer2.DOFillAmount(0.73f, _layer2Duration);
                _bottomLayer2.DOFillAmount(0.73f, _layer2Duration);
            });
            seq.Append(_goToSelectScenePanel.DOFade(1, _goToSelectPanelDuration))
                .AppendCallback(() =>
                {
                    _goToSelectScenePanel.blocksRaycasts = true;
                    _goToSelectScenePanel.interactable = true;
                    _canvasGroup.blocksRaycasts = true;
                    _canvasGroup.interactable = true;
                });


        }
        public void Close()
        {
        }
    }

}