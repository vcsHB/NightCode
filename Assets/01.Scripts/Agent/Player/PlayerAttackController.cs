using System;
using Agents.Players.SkillSystem;
using Agents.Players.WeaponSystem;
using Core.DataControl;
using UnityEngine;
namespace Agents.Players
{
    public class PlayerAttackController : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private PlayerWeaponListSO _weaponListSO;
        [SerializeField] private PlayerWeaponSO _weaponSO;
        public event Action<float, float> OnSkillCooltimeUpdateEvent;

        private Player _player;
        private PlayerWeapon _weapon;
        private PlayerSkill _skill;
        public PlayerWeaponSO WeaponData => _weaponSO;


        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _player.OnPlayerGenerateEvent += InitPlayerWeapon;

        }

        private void InitPlayerWeapon()
        {
            PlayerWeaponSO weaponSO = DataLoader.Instance.GetWeapon((CharacterEnum)_player.ID);
            SetWeaponSO(weaponSO);
        }
        public void AfterInit() { }

        public void Dispose()
        {
        }

        public void SetWeaponSO(PlayerWeaponSO data)
        {
            // # PlayerWeapon Initialize
            _weaponSO = data;
            _weapon = Instantiate(_weaponSO.weaponPrefab, transform);
            _weapon.Initialize(_player, _weaponSO.normalSkillCostEnergy);

            // # PlayerSkill Initialize
            PlayerSkillSO skillSO = _weaponSO.skillSO;
            if (skillSO == null || skillSO.skillPrefab == null) return;

            _skill = Instantiate(skillSO.skillPrefab, transform);
            _skill.Initialize(_player, _weapon, skillSO.skillCostEnergy, skillSO.skillCooltime);
            _player.PlayerInput.OnUseEvent += _skill.HandleUseSkill;
            _skill.OnCooltimeUpdateEvent += HandleUpdateSkill;
        }


        private void HandleUpdateSkill(float current, float max)
        {
            OnSkillCooltimeUpdateEvent?.Invoke(current, max);
        }
    }
}