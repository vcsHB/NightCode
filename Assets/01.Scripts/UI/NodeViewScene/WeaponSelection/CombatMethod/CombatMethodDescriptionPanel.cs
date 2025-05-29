using DG.Tweening;
using TMPro;
using UnityEngine;
namespace UI.NodeViewScene.WeaponSelectionUIs
{

    public class CombatMethodDescriptionPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private float _enableWidth = 300f;
        [SerializeField] private float _enableDuration = 0.3f;
        private RectTransform _rectTrm;

        private void Awake()
        {
            _rectTrm = transform as RectTransform;
        }


        public void SetDescription(string description)
        {
            _descriptionText.text = description;
        }

        public void Open()
        {
            _rectTrm.DOSizeDelta(new Vector2(_enableWidth, _rectTrm.sizeDelta.y), _enableDuration);
        }

        public void Close()
        {
            _rectTrm.DOSizeDelta(new Vector2(0f, _rectTrm.sizeDelta.y), _enableDuration);
        }
    }
}