using Agents;
using Agents.Players;
using UnityEngine;

namespace Combat.SubWeaponSystem
{
    // SubWeaponController -> SubWepaon -> WeaponObject
    public class SubWeaponController : MonoBehaviour, IAgentComponent
    {
        [Header("Controller Setting")]
        [SerializeField] private AgentRenderer _ownerRenderer;
        [SerializeField] private float _targetAutoDetectRadius = 20f;
        [SerializeField] private LayerMask _autoTargetLayer;
        private SubWeapon _weapon;
        private Transform _currentTarget;
        private bool _isTargetDetected;
        private Player _player;
        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _player.GetCompo<AgentRenderer>();

        }
        public void AfterInit() { }

        public void Dispose() { }

        private void Update()
        {
            DetectTarget();
        }

        public void UseWeapon()
        {

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


        public void SetSubWeaponData(SubWeaponSO data)
        {
            if (_weapon == null) return;
            _weapon = Instantiate(data.subWeaponPrefab, transform);

        }


    }

}