using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.InGame.GameUI.QuestSyetem
{
    public class QuestPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private float _activeHeight;
        [SerializeField] private float _activeDuration;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTrm;
        [SerializeField] private TextMeshProUGUI _titleText;

        private void Awake()
        {
            _rectTrm = transform as RectTransform;
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        [ContextMenu("DebugClose")]
        public void Close()
        {
            _rectTrm.DOSizeDelta(new Vector2(_rectTrm.sizeDelta.x, 0f), _activeDuration);
            _canvasGroup.DOFade(0f, _activeDuration).SetEase(Ease.OutQuad);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
        [ContextMenu("DebugOpen")]
        public void Open()
        {
            _rectTrm.DOSizeDelta(new Vector2(_rectTrm.sizeDelta.x, _activeHeight), _activeDuration);
            _canvasGroup.DOFade(1f, _activeDuration).SetEase(Ease.InSine);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}
