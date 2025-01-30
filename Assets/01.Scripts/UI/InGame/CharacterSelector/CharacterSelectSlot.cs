using Agents.Players;
using Combat.PlayerTagSystem;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame.GameUI.CharacterSelector
{

    public class CharacterSelectSlot : MonoBehaviour
    {
        [SerializeField] private PlayerSO playerSO;

        [SerializeField] private Image _playerIconImage;

        public void SelectCharacter(PlayerSO player)
        {
            playerSO = player;
            _playerIconImage.sprite = playerSO.characterIconSprite;
            
        }   


    }
}