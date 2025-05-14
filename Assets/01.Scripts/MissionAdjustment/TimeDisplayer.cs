using TMPro;
using UnityEngine;
namespace UI.Common
{

    public class TimeDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;

        private void Awake()
        {
            if (_timeText == null)
                _timeText = GetComponent<TextMeshProUGUI>();
        }

        public void SetTimeText(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            int milliseconds = Mathf.FloorToInt((time * 100f) % 100f); // 

            _timeText.text = $"{minutes:00}:{seconds:00}.{milliseconds:00}";
        }

    }
}