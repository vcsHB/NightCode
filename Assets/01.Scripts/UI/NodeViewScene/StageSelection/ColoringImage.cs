using Core.Attribute;
using UnityEngine;
using UnityEngine.UI;

namespace UI.NodeViewScene.StageSelectionUIs
{
    public class ColoringImage : MonoBehaviour
    {
        [SerializeField] private bool _keepAlpha = true;
        [SerializeField] private bool _useBlendColor;
        [ShowIf(nameof(_useBlendColor)), SerializeField] private Color _blendColor;
        [ShowIf(nameof(_useBlendColor)), Range(0f, 1f), SerializeField] private float _blendOpacity = 0.5f;

        private Image _imageCompo;

        private void Awake()
        {
            _imageCompo = GetComponent<Image>();
        }

        public void SetColor(Color color)
        {
            Color finalColor;

            if (_useBlendColor)
            {
                finalColor = Color.Lerp(color, _blendColor, _blendOpacity);
            }
            else
            {
                finalColor = color;
            }

            if (_keepAlpha)
            {
                float originalAlpha = _imageCompo.color.a;
                finalColor.a = originalAlpha;
            }

            _imageCompo.color = finalColor;
        }
    }
}
