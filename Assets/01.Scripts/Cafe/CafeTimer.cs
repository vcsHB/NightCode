using System.Text;
using TMPro;
using UnityEngine;

namespace Base.Cafe
{
    public class CafeTimer : MonoBehaviour
    {
        public TextMeshProUGUI timerText;

        private StringBuilder sb;

        private void Update()
        {
            float remainTime = CafeManager.Instance.cafeSO.openTime - CafeManager.Instance.CurrentTime;
            remainTime = Mathf.Clamp(remainTime, 0, CafeManager.Instance.cafeSO.openTime);

            StringBuilder sb = new StringBuilder();
            sb.Append("남은시간: ");
            sb.Append(Mathf.FloorToInt(remainTime / 60));
            sb.Append(":");
            sb.Append(string.Format("{0,2:D2}", Mathf.FloorToInt(remainTime % 60)));

            timerText.SetText(sb);
        }
    }
}

