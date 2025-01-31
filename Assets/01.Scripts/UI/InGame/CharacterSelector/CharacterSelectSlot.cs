using System;
using Agents.Players;
using Combat.PlayerTagSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame.GameUI.CharacterSelector
{

    public class CharacterSelectSlot : MonoBehaviour
    {
        [SerializeField] private PlayerSO _playerSO;
        [SerializeField] private Player _player;

        [SerializeField] private Image _playerIconImage;
        [SerializeField] private TextMeshProUGUI _characterNameText;
        [SerializeField] private Image _healthGauge;
        [SerializeField] private Gradient _healthFillColorLevel;

        public int PlayerId => _playerSO.id;


        public void SelectCharacter(PlayerSO playerSO, Player player)
        {
            _playerSO = playerSO;
            _player = player;
            _playerIconImage.sprite = playerSO.characterIconSprite;

            _player.HealthCompo.OnHealthChangedValueEvent += HandleHealthChange;
            
        }

        private void HandleHealthChange(float current, float max)
        {
            float ratio = Mathf.Clamp01(current / max);
            _healthGauge.color = _healthFillColorLevel.Evaluate(ratio);
            _healthGauge.fillAmount = ratio;
        }


    }
}