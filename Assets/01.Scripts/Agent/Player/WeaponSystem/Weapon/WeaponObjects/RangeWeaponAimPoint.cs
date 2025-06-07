using UnityEngine;
namespace Agents.Players.WeaponSystem.Weapon.WeaponObjects
{

    public class RangeWeaponAimPoint : MonoBehaviour
    {
        private SpriteRenderer _aimPointRenderer;

        private void Awake()
        {
            _aimPointRenderer = GetComponentInChildren<SpriteRenderer>();

        }

        public void SetAimPointEnable(bool value)
        {
            _aimPointRenderer.enabled = value;
        }

        public void SetAimPoint(Vector2 position)
        {
            transform.position = position;

        }
    }
}