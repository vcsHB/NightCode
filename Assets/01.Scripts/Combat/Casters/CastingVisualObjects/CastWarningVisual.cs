using UnityEngine;
namespace Combat.Casters.CastingVisuals
{

    public class CastWarningVisual : MonoBehaviour
    {
        [SerializeField] private CircleCaster _circleCaster;
        private SpriteRenderer _visualRenderer;
        private void Awake()
        {
            _visualRenderer = GetComponent<SpriteRenderer>();
            
            if (_visualRenderer == null)
                _visualRenderer = GetComponentInChildren<SpriteRenderer>();
            _visualRenderer.transform.localScale = Vector3.one * _circleCaster.DetectRadius * 2f;
        }

        public void SetActiveVisual(bool value)
        {
            _visualRenderer.enabled = value;
            
        }
    }
}