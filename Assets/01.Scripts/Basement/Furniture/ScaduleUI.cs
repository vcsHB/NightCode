using Basement.Training;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Basement
{
    public class ScaduleUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TrainingStatePanel _trainingStatePanel;
        [SerializeField] private RectTransform _trainingStatePanelParent;
        private bool _isOpen = false;
        private bool _isMouseEnter = false;

        private Sequence _seq;
        private Dictionary<CharacterEnum, TrainingStatePanel> trainingPanel;

        public void Start()
        {
            trainingPanel = new Dictionary<CharacterEnum, TrainingStatePanel>();
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && !_isMouseEnter && _isOpen)
            {
                if (_seq != null && _seq.active)
                    _seq.Kill();

                Vector2 position
                    = new Vector2(-_trainingStatePanelParent.rect.width / 2, _trainingStatePanelParent.rect.height / 2);

                _seq = DOTween.Sequence();
                _seq.Append(_trainingStatePanelParent.DOScale(0, 0.2f))
                    .Join(_trainingStatePanelParent.DOAnchorPos(position, 0.2f));
                _isOpen = false;
            }

        }

        public void OnClick()
        {
            if (_isOpen == false)
            {
                if (_seq != null && _seq.active)
                    _seq.Kill();

                _seq = DOTween.Sequence();
                _seq.Append(_trainingStatePanelParent.DOScale(1, 0.2f))
                    .Join(_trainingStatePanelParent.DOAnchorPos(Vector2.zero, 0.2f));
                _isOpen = true;

                OnUpdateScadule();
            }
        }

        private void OnUpdateScadule()
        {
            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                TrainingStatePanel panel;

                if (trainingPanel.TryGetValue(character, out panel))
                {
                    if (WorkManager.Instance.TryGetTrainingInfo(character, out RoomActionInfo info))
                    {
                        panel.SetInfo(character, info.remainTime, info.displayAction);
                        LayoutRebuilder.ForceRebuildLayoutImmediate(_trainingStatePanelParent);
                    }
                    else
                    {
                        Destroy(trainingPanel[character].gameObject);
                        trainingPanel.Remove(character);
                    }
                }
                else
                {
                    if (WorkManager.Instance.TryGetTrainingInfo(character, out RoomActionInfo training))
                    {
                        panel = Instantiate(_trainingStatePanel, _trainingStatePanelParent);
                        panel.SetInfo(character, training.remainTime, training.displayAction);
                        LayoutRebuilder.ForceRebuildLayoutImmediate(_trainingStatePanelParent);
                        trainingPanel.Add(character, panel);
                    }

                }
            }

            trainingPanel.Keys.ToList().ForEach(character =>
            {
                if (WorkManager.Instance.TryGetTrainingInfo(character, out RoomActionInfo info))
                {
                    var panel = trainingPanel[character];
                    panel.SetInfo(character, info.remainTime, info.displayAction);
                }
            });
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isMouseEnter = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isMouseEnter = false;
        }
    }
}
