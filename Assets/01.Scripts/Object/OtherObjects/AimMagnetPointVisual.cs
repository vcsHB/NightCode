using UnityEngine;
namespace ObjectManage
{

    public class AimMagnetPointVisual : MonoBehaviour
    {
        private SpriteRenderer _visualRenderer;
        private bool _isActive;
        private void Awake()
        {
            _visualRenderer = GetComponent<SpriteRenderer>();
        }


        public void SetEnabled(bool value)
        {
            _isActive = value;
            _visualRenderer.enabled = value;
        }
    }
}