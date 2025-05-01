using System;
using Combat;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame
{
    [Serializable]
    public struct PhaseColor
    {
        [Range(0f, 1f)] public float healthRatio;
        public Color color;
    }
    public class SliderHealthBar : MonoBehaviour
    {
        [SerializeField] private Health _ownerHealth;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _gaugeFillImage;
        [SerializeField] private PhaseColor[] _phaseColorSet;

        private void Awake()
        {
            _ownerHealth.OnHealthChangedValueEvent += HandleChangeHealth;
        }

        public void HandleChangeHealth(float current, float max)
        {
            float ratio = current / max;
            for (int i = 0; i < _phaseColorSet.Length; i++)
            {
                if (ratio <= _phaseColorSet[i].healthRatio)
                {
                    _gaugeFillImage.color = _phaseColorSet[i].color;
                    break;
                }
            }
            _slider.value = current / max;
        }
    }
}