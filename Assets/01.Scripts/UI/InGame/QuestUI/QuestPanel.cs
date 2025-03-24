using DG.Tweening;
using QuestSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.InGame.GameUI.QuestSyetem
{
    public class QuestPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private QuestManager _questManager;
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
        [SerializeField] private TextMeshProUGUI _descriptionText;

        [Header("Progress Displayers")]
        [SerializeField] private ProgressDisplayer[] _progressDisplayers;
        private readonly int _labelUnscaledTimeHash = Shader.PropertyToID("_CurrentUnscaledTime");
        private bool _isActive;
        private void Awake()
        {
            _rectTrm = transform as RectTransform;
            _canvasGroup = GetComponent<CanvasGroup>();

            _labelMaterial1 = _topLabelImage.material;
            _labelMaterial2 = _bottomLabelImage.material;

            _questManager.OnQuestChangeEvent += HandleQuestSetEvent;
        }

        private void OnDestroy()
        {
            _questManager.OnQuestChangeEvent -= HandleQuestSetEvent;

        }

        private void Update()
        {
            if (_isActive)
            {
                RefreshLabelsMaterial();
            }
        }
        #region OnOff Control

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
            HandleQuestSetEvent(_questManager.CurrentQuest);
            _isActive = true;
        }

        #endregion

        private void RefreshLabelsMaterial()
        {
            _labelMaterial1.SetFloat(_labelUnscaledTimeHash, Time.unscaledTime);
            _labelMaterial2.SetFloat(_labelUnscaledTimeHash, Time.unscaledTime);
        }

        private void HandleQuestSetEvent(QuestSO quest)
        {
            QuestData data = _questManager.CurrentQuestData;

            _descriptionText.text = quest.description;
            _titleText.text = quest.questName;
            for (int i = 0; i < _progressDisplayers.Length; i++)
                _progressDisplayers[i].SetDisable();

            ProgressDisplayer displayer = _progressDisplayers[(int)data.questType];
            switch (quest.questType)
            {
                case QuestType.KillSingleTarget:
                case QuestType.KillMultiTarget:
                    displayer.SetQuestData(quest);
                    break;
                case QuestType.Rescue:
                    // 구조대상 설정
                    break;
            }
            displayer.SetProgress(data);
            displayer.SetEnable();
        }

    }
}
