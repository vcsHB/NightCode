using UnityEngine;
namespace Combat.Casters.CastingVisuals
{

    public class CastWarningVisual : MonoBehaviour
    {
        private CircleCaster _circleCaster;
        private SpriteRenderer _visualRenderer;
        private void Awake()
        {
            _circleCaster = GetComponent<CircleCaster>();
            _visualRenderer = GetComponent<SpriteRenderer>();
            
            if (_visualRenderer == null)
                _visualRenderer = GetComponentInChildren<SpriteRenderer>();
            _visualRenderer.transform.localScale = Vector3.one * _circleCaster.DetectRadius;
        }

        public void SetActiveVisual(bool value)
        {
            _visualRenderer.enabled = value;
            
        }
    }
}