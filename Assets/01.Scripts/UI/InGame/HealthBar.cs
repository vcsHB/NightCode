using Agents;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame
{

    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _ownerHealth;
        [SerializeField] private Image _guageFill;

        private void Awake()
        {
            _ownerHealth.OnHealthChangedValueEvent += HandleChangeHealth;
        }

        public void HandleChangeHealth(float current, float max)
        {
            _guageFill.fillAmount = current / max;
        }
    }
}