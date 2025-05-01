using TMPro;
using UnityEngine;

namespace Base.Cafe
{
    public class ProgressBar : MonoBehaviour
    {
        public Transform pivot;
        public TextMeshPro timeText;

        private float _pivotYSize => pivot.localScale.y;

        public void SetText(string text)
            => timeText.SetText(text);

        public void SetFillAmount(float value)
            => pivot.localScale = new Vector3(Mathf.Clamp(value, 0, 1), _pivotYSize, 1);
    }
}
