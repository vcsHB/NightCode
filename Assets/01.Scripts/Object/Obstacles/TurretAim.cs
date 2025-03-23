using UnityEngine;
namespace ObjectManage.Obstacles
{

    public class TurretAim : MonoBehaviour
    {
        private SpriteRenderer _aimRenderer;

        private void Awake()
        {
            _aimRenderer = GetComponentInChildren<SpriteRenderer>();

        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetActive(bool value)
        {
            _aimRenderer.enabled = value;
        }
    }
}