using Agents;
using Agents.Players;
using UnityEditor.Build;
using UnityEngine;

namespace CameraControllers.CameraHolders
{

    public class CameraHolder : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private float _holderRange;
        [SerializeField] private LayerMask _holdPointLayer;
        private Player _player;
        private CameraManager _cameraManager;

        void Start()
        {
            _cameraManager = CameraManager.Instance;
        }

        private void FixedUpdate()
        {
            if (_player.IsActive)
                CheckHoldPoint();
        }

        private void CheckHoldPoint()
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, _holderRange, _holdPointLayer);
            if (target != null)
            {
                _cameraManager.SetFollow(target.transform);
            }
            else
            {
                _cameraManager.SetFollow(_player.transform);

            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, _holderRange);
        }

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
        }

        public void AfterInit() { }

        public void Dispose() { }

#endif
    }

}