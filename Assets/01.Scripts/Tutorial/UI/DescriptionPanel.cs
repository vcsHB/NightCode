using UnityEngine;
using UnityEngine.UI;

namespace UI.Tutorial
{

    public class DescriptionPanel : UIPanel
    {
        [SerializeField] private Image _topLineImage;
        [SerializeField] private Image _bottomeLineImage;
        private int _lineUnscaledTimeHash = Shader.PropertyToID("_CurrentUnscaledTime");

        private void Update()
        {

            if (_isActive)
            {
                _topLineImage.material.SetFloat(_lineUnscaledTimeHash, Time.unscaledTime);
                _bottomeLineImage.material.SetFloat(_lineUnscaledTimeHash, Time.unscaledTime);
            }
        }
    }

}