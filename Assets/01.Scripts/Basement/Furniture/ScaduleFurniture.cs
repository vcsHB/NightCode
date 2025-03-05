using Basement.Training;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class ScaduleFurniture : Furniture
    {
        [SerializeField] private TrainingStatePanel _trainingStatePanel;
        [SerializeField] private Transform _trainingStatePanelParent;

        private Dictionary<CharacterEnum, TrainingStatePanel> trainingPanel;

        public void Start()
        {
            trainingPanel = new Dictionary<CharacterEnum, TrainingStatePanel>();
            TrainingManager.Instance.OnAddScadule += OnAddScadule;
            TrainingManager.Instance.OnRemoveScadule += OnRemoveScadule;
            TrainingManager.Instance.OnUpdateScadule += OnUpdateScadule;
        }

        private void OnDisable()
        {
            if (TrainingManager.Instance == null) return;

            TrainingManager.Instance.OnAddScadule -= OnAddScadule;
            TrainingManager.Instance.OnRemoveScadule -= OnRemoveScadule;
            TrainingManager.Instance.OnUpdateScadule -= OnUpdateScadule;
        }

        private void OnAddScadule(CharacterEnum character, TrainingInfo training)
        {
            TrainingStatePanel panel = Instantiate(_trainingStatePanel, _trainingStatePanelParent);
            panel.SetInfo(character, training.remainTime, training.training.trainingVisibleName);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_trainingStatePanelParent as RectTransform);

            trainingPanel.Add(character, panel);
        }

        private void OnRemoveScadule(CharacterEnum characterEnum)
        {
            Destroy(trainingPanel[characterEnum].gameObject);
            trainingPanel.Remove(characterEnum);
        }

        private void OnUpdateScadule()
        {
            trainingPanel.Keys.ToList().ForEach(character =>
            {
                if (TrainingManager.Instance.characterTrainingInfo.TryGetValue(character, out TrainingInfo info))
                {
                    var panel = trainingPanel[character];
                    panel.SetInfo(character, info.remainTime, info.training.trainingName);
                }
            });
        }
    }
}
