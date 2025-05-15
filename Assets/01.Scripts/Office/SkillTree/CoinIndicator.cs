using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Office.CharacterSkillTree
{
    public class CoinIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private TextMeshProUGUI _explainText;
        private RectTransform RectTrm => transform as RectTransform;

        public void SetIndicator(int coin, List<NodeSO> nodes)
        {
            gameObject.SetActive(true);
            RectTrm.anchoredPosition = Mouse.current.position.ReadValue();
            _coinText.SetText($"{coin}포인트 필요");

            StringBuilder sb = new StringBuilder();

            nodes.ForEach(node =>
            {
                sb.Append(node.explain);
                sb.Append("\n");
            });

            _explainText.SetText(sb.ToString());
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
