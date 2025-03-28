using System;
using Agents;
using Agents.Players;
using UnityEngine;

namespace Combat.SubWeaponSystem
{

    /// <summary>
    /// basic Weapon Manage Controller. (AgentComponent)
    /// manage relationship order
    /// [WeaponController] -> Weapon -> WeaponObject
    /// </summary>
    public class SubWeaponController : MonoBehaviour, IAgentComponent
    {
        [Header("Controller Setting")]
        [SerializeField] private AgentRenderer _ownerRenderer;
        [SerializeField] private float _targetAutoDetectRadius = 20f;
        [SerializeField] private LayerMask _autoTargetLayer;
        [SerializeField] private SubWeaponSO _currentWeapon;
        private SubWeapon _weapon;
        private Transform _currentTarget;
        private bool _isTargetDetected;
        private Player _player;

        private void Start()
        {
            DebugSetWeapon();
        }

        #region AgentCompo Functions
        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _player.GetCompo<AgentRenderer>();

            _player.OnEnterEvent += HandlePlayerEnter;
            _player.OnExitEvent += HandlePlayerExit;

        }
        public void AfterInit() { }

        public void Dispose()
        {
            _player.OnEnterEvent -= HandlePlayerEnter;
            _player.OnExitEvent -= HandlePlayerExit;
        }

        #endregion

        private void HandlePlayerEnter()
        {
            _player.PlayerInput.OnUseEvent += UseWeapon;
        }
        private void HandlePlayerExit()
        {
            _player.PlayerInput.OnUseEvent -= UseWeapon;
        }




        private void Update()
        {
            
            DetectTarget();
        }

        public void UseWeapon()
        {
            if(!_player.IsActive) return;
            if(!_weapon.CanUse) return;
            Vector2 targetDirection = _isTargetDetected ?
                (_currentTarget.position - transform.position).normalized :
                new Vector2(_ownerRenderer.FacingDirection, 0f);
            _weapon.UseWeapon(new SubWeaponControlData()
            {
                direction = targetDirection,
                damage = 1
            });

        }

        private void DetectTarget()
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, _targetAutoDetectRadius, _autoTargetLayer);
            _isTargetDetected = target != null;
            if (!_isTargetDetected) return;
            _currentTarget = target.transform;
        }
        private void DebugSetWeapon()
        {
            SetSubWeaponData(_currentWeapon);
        }
        public void SetSubWeaponData(SubWeaponSO data)
        {
            if (data == null) return;
            _currentWeapon = data;
            _weapon = Instantiate(data.subWeaponPrefab, transform);

        }


    }

}