using UnityEngine;
namespace Agents.Enemies.Highbinders
{
    public class Highbinder : GrabableEnemy
    {
        private int _deadBodyLayer = 10;
        private int _defaultLayer;

        protected override void Awake()
        {
            base.Awake();
            _defaultLayer = gameObject.layer;
            
        }

        protected override void HandleAgentDie()
        {
            base.HandleAgentDie();
            gameObject.layer = _deadBodyLayer;

        }
    }
}