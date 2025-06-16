using UnityEngine;
namespace Agents.Enemies.Highbinders
{

    public class LaserAimLine : MonoBehaviour
    {
        private Transform _visualTrm;
        private SpriteRenderer _lineVisualRenderer;
        private void Awake()
        {
            _visualTrm = transform.Find("Visual");
            _lineVisualRenderer = _visualTrm.GetComponent<SpriteRenderer>();
        }

        public void SetLineEnable(bool value)
        {
            _lineVisualRenderer.enabled = value;
        }
        public void SetDirection(Vector2 direction)
        {
            transform.right = direction.normalized;
        }
    }
}