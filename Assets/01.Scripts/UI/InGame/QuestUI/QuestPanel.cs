using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.InGame.GameUI.QuestSyetem
{
    public class QuestPanel : MonoBehaviour, IWindowPanel
    {
        public UnityEvent OnOpenEvent;
        public UnityEvent OnCloseEvent;
        [SerializeField] private float _activeHeight;
        [SerializeField] private float _activeDuration;
        [SerializeField] private Image _topLabelImage;
        [SerializeField] private Image _bottomLabelImage;
        private Material _labelMaterial1;
        private Material _labelMaterial2;

        private CanvasGroup _canvasGroup;
        private RectTransform _rectTrm;
        [SerializeField] private TextMeshProUGUI _titleText;
        
        private readonly int _labelUnscaledTimeHash = Shader.PropertyToID("_CurrentUnscaledTime");
        private bool _isActive;
        private void Awake()
        {
            _rectTrm = transform as RectTransform;
            _canvasGroup = GetComponent<CanvasGroup>();

            _labelMaterial1 = _topLabelImage.material;
            _labelMaterial2 = _bottomLabelImage.material;
        }

        private void Update()
        {
            if(_isActive)
            {
                RefreshLabelsMaterial();
            }
        }

        [ContextMenu("DebugClose")]
        public void Close()
        {
            _rectTrm.DOSizeDelta(new Vector2(_rectTrm.sizeDelta.x, 0f), _activeDuration).SetUpdate(true);
            _canvasGroup.DOFade(0f, _activeDuration).SetEase(Ease.OutQuad).SetUpdate(true);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            OnCloseEvent?.Invoke();
            _isActive = false;
        }
        [ContextMenu("DebugOpen")]
        public void Open()
        {
            _rectTrm.DOSizeDelta(new Vector2(_rectTrm.sizeDelta.x, _activeHeight), _activeDuration).SetUpdate(true);
            _canvasGroup.DOFade(1f, _activeDuration).SetEase(Ease.InSine).SetUpdate(true);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            OnOpenEvent?.Invoke();
            _isActive = true;
        }

        private void RefreshLabelsMaterial()
        {
            _labelMaterial1.SetFloat(_labelUnscaledTimeHash, Time.unscaledTime);
            _labelMaterial2.SetFloat(_labelUnscaledTimeHash, Time.unscaledTime);
        }
    }
}
