using UnityEngine;
namespace Agents.Players.WeaponSystem.Weapon.WeaponObjects
{

    public class RangeWeaponAimVisual : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private RangeWeaponAimPoint _aimPoint;


        public void SetAimToTarget(Transform targetTrm)
        {

            _aimPoint.SetAimPoint(targetTrm.position);
        }


    }
}