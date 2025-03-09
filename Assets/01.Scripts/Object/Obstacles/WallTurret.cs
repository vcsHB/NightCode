using Combat.CombatObjects.ProjectileManage;
using UnityEngine;

namespace ObjectManage.Obstacles
{

    public class WallTurret : MonoBehaviour
    {
        [SerializeField] private ProjectileShooter _shooter;

        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _targetDetectRadius;

        [SerializeField] private float _fireCooltime;

        [SerializeField] private Transform _headTrm;
    }
}