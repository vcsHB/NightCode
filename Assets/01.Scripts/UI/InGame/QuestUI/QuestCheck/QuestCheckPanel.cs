using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame.GameUI
{

    public class QuestCheckPanel : UIPanel
    {
        [SerializeField] private Image _topLabelImage;
        [SerializeField] private Image _bottomLabelImage;
        private Material _labelMaterial1;
        private Material _labelMaterial2;
        private readonly int _labelUnscaledTimeHash = Shader.PropertyToID("_CurrentUnscaledTime");

        protected override void Awake()
        {
            base.Awake();
            _labelMaterial1 = _topLabelImage.material;
            _labelMaterial2 = _bottomLabelImage.material;
        }

        private void Update()
        {
            if (_isActive)
            {
                RefreshLabelsMaterial();
            }
        }


        private void RefreshLabelsMaterial()
        {
            _labelMaterial1.SetFloat(_labelUnscaledTimeHash, Time.unscaledTime);
            _labelMaterial2.SetFloat(_labelUnscaledTimeHash, Time.unscaledTime);
        }

    }

}