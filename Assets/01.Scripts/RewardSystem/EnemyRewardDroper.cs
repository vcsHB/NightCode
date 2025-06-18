using Agents;
using Agents.Players;
using Core.DataControl;
using UnityEngine;

namespace RewardSystem
{
    public class EnemyRewardDroper : MonoBehaviour
    {
        [SerializeField] private int _energy = 10;
        [SerializeField] private int _gainCredit = 50;
        private Agent _owner;
        [Header("Collector Settings")]
        [SerializeField] private LayerMask _collectLayer;
        [SerializeField] private float _detectRadius;

        private void Awake()
        {

            _owner = GetComponentInParent<Agent>();
            _owner.OnDieEvent += HandleOwnerDie;
        }

        private void HandleOwnerDie()
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, _detectRadius, _collectLayer);
            if (target == null) return;
            if (target.TryGetComponent(out Player player))
            {
                player.EnergyController.GainEnergy(_energy);
            }
            CreditCollector.Instance.CollectCredit(_gainCredit);
        }
    }

}