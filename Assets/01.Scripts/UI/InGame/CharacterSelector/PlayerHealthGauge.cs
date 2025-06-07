using System;
using Agents.Players;
using UI.InGame.GameUI.CharacterSelector;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame.GameUI.CharacterSelector
{

    public class PlayerHealthGauge : MonoBehaviour
    {
        [SerializeField] private Slider _healthGaugeSlider;
        private CharacterSelectWindow _charatcerSelector;
        private Player _previousPlayer;

        private void Start()
        {
            _charatcerSelector = GetComponentInParent<CharacterSelectWindow>();
            _charatcerSelector.OnPlayerSelectEvent += HandleCharacterSelect;
        }

        private void HandleCharacterSelect(Player player)
        {
            if (_previousPlayer != null)
            {
                _previousPlayer.HealthCompo.OnHealthChangedValueEvent -= HandleHealthChanged;
            }

            player.HealthCompo.OnHealthChangedValueEvent += HandleHealthChanged;
            HandleHealthChanged(player.HealthCompo.CurrentHealth, player.HealthCompo.MaxHealth);
        }

        private void HandleHealthChanged(float current, float max)
        {
            float ratio = current / max;
            _healthGaugeSlider.value = ratio;
        }
    }
}