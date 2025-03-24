using Combat.PlayerTagSystem;
using UnityEngine;
namespace Dialog.SituationControl
{

    public class CharacterJoinController : SituationElement
    {
        [SerializeField] private PlayerSO _playerSO;

        public override void EndSituation()
        {
        }

        public override void StartSituation()
        {
            PlayerManager.Instance.AddPlayer(_playerSO);
        }
    }
}