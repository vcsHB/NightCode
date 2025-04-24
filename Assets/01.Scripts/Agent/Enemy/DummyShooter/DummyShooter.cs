using Agents.Enemies.Highbinders;
using ObjectManage.VFX;
using UnityEngine;

namespace Agents.Enemies.DummyShooter
{
    public class DummyShooter : Highbinder
    {
        protected override void HandleAgentDie()
        {
            base.HandleAgentDie();
            DebrisObject debris = PoolManager.Instance.Pop(ObjectPooling.PoolingType.RobotDebrisVFX) as DebrisObject;
            debris.transform.position = transform.position;
            debris.Play();
        }
    }

}