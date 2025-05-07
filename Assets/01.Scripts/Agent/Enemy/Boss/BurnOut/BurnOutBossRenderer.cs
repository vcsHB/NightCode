using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace Agents.Enemies.BossManage
{

    public class BurnOutBossRenderer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer[] _renderers;
        [SerializeField] private Light2D[] _energyLights;
        [SerializeField] private Material _material;
        private readonly int _emissionColorHash = Shader.PropertyToID("_EmissionColor");
        [SerializeField, ColorUsage(true, true)] private Color _defaultEmissionColor;
        private Color _disableColor = Color.black;


        private void Awake()
        {
            _material = Instantiate(_material);
            
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].sharedMaterial = _material;
            }
        }

        public void SetDeadStateVisual(bool value)
        {
            for (int i = 0; i < _energyLights.Length; i++)
            {
                _energyLights[i].enabled = !value;
            }
            _material.SetColor(_emissionColorHash, value ? _disableColor : _defaultEmissionColor);

        }
    }
}