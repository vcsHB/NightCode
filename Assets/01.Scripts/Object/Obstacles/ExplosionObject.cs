using Combat;
using UnityEngine;
namespace ObjectManage.Obstacles
{

    public class ExplosionObject : Obstacle
    {
        private Caster _caster;

        public void Explode()
        {
            _caster.Cast();
        }
    }
}