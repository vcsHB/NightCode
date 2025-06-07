using System.Collections.Generic;
using Map;
using TMPro;
using UnityEngine;

namespace UI.NodeViewScene.StageSelectionUIs
{
    public class DifficultyDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _difficultyText;
        [SerializeField] private DifficultySlot _difficultySlot;
        [SerializeField] private Transform _contentTrm;
        private Queue<DifficultySlot> _slotPool = new Queue<DifficultySlot>();

        public void SetDifficulty(StageDifficultySO data)
        {
            ResetSlots();
            for (int i = 0; i < data.level; i++)
            {
                DifficultySlot slot = GetSlot();
                slot.SetActive(true);
                slot.SetColor(data.difficultyColor);

            }
            _difficultyText.text = data.difficultyName;
        }

        private DifficultySlot GetSlot()
        {
            if (_slotPool.Count > 0)
            {
                DifficultySlot reused = _slotPool.Dequeue();
                reused.gameObject.SetActive(true);
                return reused;
            }

            DifficultySlot newSlot = Instantiate(_difficultySlot, _contentTrm);
            return newSlot;
        }

        public void ResetSlots()
        {
            foreach (Transform child in _contentTrm)
            {
                DifficultySlot slot = child.GetComponent<DifficultySlot>();
                if (slot != null)
                {
                    slot.SetActive(false);
                    _slotPool.Enqueue(slot);
                }
            }
        }
    }
}
