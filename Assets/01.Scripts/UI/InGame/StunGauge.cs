using System;
using Combat;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame
{

    public class StunGauge : MonoBehaviour
    {
        [SerializeField] private StunBody _ownerBody;
        [SerializeField] private Image _gaugeFillImage;

        private void Awake()
        {
            _ownerBody.OnStunLevelChangedEvent += HandleRefreshStunGauge;
            _ownerBody.OnStunCompleteEvent.AddListener(HandleStunCompletly);
        }

        private void HandleRefreshStunGauge(float currentValue, float maxValue)
        {
            float ratio = currentValue / maxValue;
            _gaugeFillImage.fillAmount = ratio;

        }

        private void HandleStunCompletly()
        {
             
        }


    }
}