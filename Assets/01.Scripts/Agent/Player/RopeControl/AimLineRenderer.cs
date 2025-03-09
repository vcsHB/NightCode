using System;
using UnityEngine;
namespace Agents.Players
{

    public class AimLineRenderer : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private LineRenderer _lineRenderer;
        private Player _player;

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
        }
        public void AfterInit()
        {
            _player.GetCompo<AimDetector>().OnAimEvent += HandleAimLineRefresh;
        }

        public void Dispose()
        {
        }


        private void HandleAimLineRefresh(AimData data)
        {
            _lineRenderer.enabled = data.isTargeted;
            _lineRenderer.SetPosition(0, data.originPlayerPosition);
            _lineRenderer.SetPosition(1, data.targetPosition);

            //_aimGroupController.SetVirtualAimPosition(_targetPoint);
        }

    }
}