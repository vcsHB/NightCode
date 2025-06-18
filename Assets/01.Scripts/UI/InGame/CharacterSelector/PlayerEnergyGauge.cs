using Agents.Players;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame.GameUI.CharacterSelector
{

    public class PlayerEnergyGauge : MonoBehaviour
    {

        [SerializeField] private Slider _energyGaugeSlider;
        private CharacterSelectWindow _charatcerSelector;
        private Player _currentPlayer;

        private void Start()
        {
            _charatcerSelector = GetComponentInParent<CharacterSelectWindow>();
            _charatcerSelector.OnPlayerSelectEvent += HandleCharacterSelect;
        }

        private void HandleCharacterSelect(Player player)
        {
            if (player == null)
                Debug.Log("?>??");
            if (_currentPlayer != null)
            {
                _currentPlayer.EnergyController.OnEnergyChangedEvent -= HandleEnergyChanged;
            }
            _currentPlayer = player;
            player.EnergyController.OnEnergyChangedEvent += HandleEnergyChanged;
            HandleEnergyChanged(player.EnergyController.CurrentEnergy, player.EnergyController.MaxEnergy);
        }

        private void HandleEnergyChanged(int current, int max)
        {
            float ratio = (float)current / max;
            _energyGaugeSlider.value = ratio;
        }
    }
}