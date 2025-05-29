using Agents.Players.WeaponSystem;
using UnityEngine;
using UnityEngine.UI;
namespace UI.NodeViewScene
{

    public class CombatMethodSlot : MonoBehaviour
    {

        [SerializeField] private CombatMethodDescriptionPanel _descriptionPanel;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _tagIconImage;



        public void SetCombatMethodData(CombatMethodTagSO combatMethodData)
        {
            _backgroundImage.color = combatMethodData.methodColor;
            _tagIconImage.sprite = combatMethodData.methodIconSprite;
            _descriptionPanel.SetDescription(combatMethodData.methodDescription);
            _descriptionPanel.Close();
        }

    }
}