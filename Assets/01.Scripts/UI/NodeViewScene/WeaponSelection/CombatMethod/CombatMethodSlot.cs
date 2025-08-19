using Agents.Players.WeaponSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.NodeViewScene.WeaponSelectionUIs
{

    public class CombatMethodSlot : MonoBehaviour
    {

        [SerializeField] private CombatMethodDescriptionPanel _descriptionPanel;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _tagIconImage;

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetCombatMethodData(CombatMethodTagSO combatMethodData)
        {
            _backgroundImage.color = combatMethodData.methodColor;
            _tagIconImage.sprite = combatMethodData.methodIconSprite;
            _nameText.text = combatMethodData.methodName;
            _descriptionPanel.SetDescription(combatMethodData.methodDescription);
            _descriptionPanel.Close();
        }

    }
}