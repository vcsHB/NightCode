using SpeedRun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.SpeedRun
{

    public class SpeedRunRankingSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _rankingText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private Color[] _rankColor;
        [SerializeField] private Image[] _coloringPanels;
        [SerializeField] private GameObject[] _top3RankerPanels;
        [SerializeField] private GameObject[] _top10RankerPanels;

        public void SetSpeedRunRecord(SpeedRunRecord record, int rank)
        {
            _rankingText.text = (rank + 1).ToString();
            _nameText.text = record.challengerName;
            float time = record.playTime;
            float sec = time % 60;
            int m = (int)time / 60;
            _timeText.text = $"{m.ToString("00")}:{sec.ToString("00.00")}";

            int colorIndex = Mathf.Clamp(rank, 0, 3);
            Color color = _rankColor[colorIndex];
            for (int i = 0; i < _coloringPanels.Length; i++)
            {
                color.a = _coloringPanels[i].color.a;
                _coloringPanels[i].color = color;
            }

            for (int i = 0; i < _top3RankerPanels.Length; i++)
            {
                _top3RankerPanels[i].SetActive(rank < 3);
            }
            for (int i = 0; i < _top10RankerPanels.Length; i++)
            {
                _top10RankerPanels[i].SetActive(rank < 10);
            }
        }

    }
}