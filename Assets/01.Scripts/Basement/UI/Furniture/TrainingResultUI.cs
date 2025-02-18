using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Basement.Training
{
    public class TrainingResultUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private Image _icon;

        public void SetResult(TrainingResult result, Color textColor, SkillPointEnum statType, int increaseValue, Sprite icon)
        {
            string text = result == TrainingResult.Success ? "성공" : result == TrainingResult.GreatSuccess ? "대성공" : "실패";
            _resultText.SetText(text);
            _resultText.color = textColor;
            _icon.sprite = icon;

            if (result == TrainingResult.Fail)
            {
                _valueText.SetText($"저런...");
            }
            else
            {
                _valueText.SetText($"{statType} Pt += {increaseValue}");
            }
        }
    }
}
