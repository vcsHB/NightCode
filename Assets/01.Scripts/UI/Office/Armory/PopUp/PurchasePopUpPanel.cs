using System;
using Combat.SubWeaponSystem;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.OfficeScene.Armory
{

    public class PurchasePopUpPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _currentCreditText;
        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private Color _canPurchaseColor;
        [SerializeField] private Color _creditShortageColor;
        [SerializeField] private Button _purchaseButton;

        [Header("Panel Active Setting")]
        [SerializeField] private RectTransform _mainPanelTrm;
        [SerializeField] private float _activeSize = 1.7f;
        [SerializeField] private float _activeDuration = 0.2f;
        private float _widthSize = 4f;
        public event Action OnPurchaseEvent;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = _mainPanelTrm.GetComponent<CanvasGroup>();
            _purchaseButton.onClick.AddListener(HandlePurchase);
        }

        public void SetPurchaseData(SubWeaponSO weaponSO, int currentCredit, Vector2 position)
        {
            transform.position = position;

            bool isEnough = weaponSO.initialPrice <= currentCredit;
            Color textColor = isEnough ? _canPurchaseColor : _creditShortageColor;
            _priceText.color = textColor;
            _currentCreditText.color = textColor;

            _priceText.text = $"-{weaponSO.initialPrice} Credits";
            _currentCreditText.text = $"{currentCredit} Credits";
        }

        public void Open()
        {
            _mainPanelTrm.DOSizeDelta(new Vector2(_widthSize, _activeSize), _activeDuration).OnComplete(() => SetCanvasGroupEnable(true));

        }

        public void Close()
        {
            _mainPanelTrm.DOSizeDelta(new Vector2(_widthSize, 0f), _activeDuration).OnComplete(() => SetCanvasGroupEnable(false));

        }

        private void SetCanvasGroupEnable(bool value)
        {
            _canvasGroup.alpha = value ? 1f : 0f;
            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;
        }

        private void HandlePurchase()
        {
            OnPurchaseEvent?.Invoke();
        }

    }
}