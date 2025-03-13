using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame.GameUI
{

    public class RetireSign : MonoBehaviour
    {
        [SerializeField] private Image _backgroundPanel;
        [SerializeField] private float _fadeInDuration;
        [SerializeField] private Image _textLabel;

        private void Awake()
        {

        }

        public void SetRetire(bool value)
        {
            _backgroundPanel.enabled = value;
            _backgroundPanel.DOFade(value ? 1f : 0f, _fadeInDuration);
            _textLabel.enabled = value;
        }
    }
}