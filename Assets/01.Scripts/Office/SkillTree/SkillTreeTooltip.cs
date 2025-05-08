using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Office.CharacterSkillTree
{
    public class SkillTreeTooltip : MonoBehaviour, IWindowPanel
    {
        public Image icon;
        public TextMeshProUGUI nodeName, explain, requireCoin;


        public RectTransform RectTrm => transform as RectTransform;


        public void Init(NodeSO node)
        {
            icon.gameObject.SetActive(node.icon != null);
            icon.sprite = node.icon;
        }


        public void Close()
        {

        }

        public void Open()
        {
        
        }
    }
}
