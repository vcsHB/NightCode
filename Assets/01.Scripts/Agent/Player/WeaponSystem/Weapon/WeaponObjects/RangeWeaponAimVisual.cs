using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players.WeaponSystem.Weapon.WeaponObjects
{

    public class RangeWeaponAimVisual : MonoBehaviour
    {
        public UnityEvent OnAimEnableEvent;
        public UnityEvent OnAimDisableEvent;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private RangeWeaponAimPoint _aimPoint;


        public void SetAimToTarget(Transform targetTrm)
        {

            _aimPoint.SetAimPoint(targetTrm.position);

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, targetTrm.position);

        }

        public void SetAimEnable(bool value)
        {
            if (value)
                OnAimEnableEvent?.Invoke();
            else
                OnAimDisableEvent?.Invoke();

            _lineRenderer.enabled = value;
            _aimPoint.SetAimPointEnable(value);
        }

    }
}