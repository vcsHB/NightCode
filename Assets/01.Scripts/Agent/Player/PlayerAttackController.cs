using Agents.Players.WeaponSystem;
using UnityEngine;
namespace Agents.Players
{
    public class PlayerAttackController : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private PlayerWeaponListSO _weaponListSO;
        [SerializeField] private PlayerWeaponSO _weaponSO;
        private Player _player;
        private PlayerWeapon _weapon;
        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            SetWeaponSO(_weaponSO); // Debug; => Connect SaveData
        }
        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void SetWeaponSO(PlayerWeaponSO data)
        {
            _weaponSO = data;
            _weapon = Instantiate(_weaponSO.weaponPrefab, transform);
            _weapon.Initialize(_player);
        }

    }
}