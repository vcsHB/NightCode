using Base;
using CameraControllers;
using Combat.SubWeaponSystem;
using DG.Tweening;
using System.Runtime.CompilerServices;
using UI;
using UI.Common;
using UI.OfficeScene.Armory;
using UnityEngine;

namespace Office.Armory
{

    public class ArmoryPanel : MonoBehaviour, IWindowPanel
    {
        private WeaponSlot[] _slots;
        [SerializeField] private BaseInput _baseInput;
        [SerializeField] private WeaponInventroyController _weaponInventory;
        [SerializeField] private CreditStorage _creditStorage;
        [SerializeField] private PurchasePopUpPanel _purchasePanel;
        [SerializeField] private WeaponInfoPanel _weaponInfoPanel;
        [SerializeField] private Transform _cameraFollow;
        [SerializeField] private GameObject _ligthParent;
        [SerializeField] private float _duration = 2f;
        [SerializeField] private SubWeaponData _debugData;

        private bool _isOpen;
        private EscCloser _escCloser;
        private SubWeaponSO _currentSelectedWeaponSO;
        private WeaponSlot _currentSelectedSlot;
        private CameraFocuser _cameraFocus;
        private CameraShakeController _cameraShakController;

        private Sequence _toggleSeq;

        private void Awake()
        {
            _purchasePanel.OnPurchaseEvent += HandlePurchaseWeapon;
            _slots = GetComponentsInChildren<WeaponSlot>();
            _cameraFocus = GetComponentInChildren<CameraFocuser>();
            _escCloser = GetComponent<EscCloser>();
            _escCloser.SetControl(false);
        }

        private void Start()
        {
            _cameraShakController = CameraManager.Instance.GetCompo<CameraShakeController>();

            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i].OnSelectEvent += HandleSlotSelected;
                if (_weaponInventory.IsUnlocked(_slots[i].WeaponSO.id))
                {
                    _slots[i].SetActive(true);
                }
                else
                    _slots[i].SetActive(false);
            }
        }
        private void OnDestroy()
        {
            _purchasePanel.OnPurchaseEvent -= HandlePurchaseWeapon;
        }


        public void Open()
        {
            if (_isOpen)
            {
                _ligthParent.SetActive(true);
                _baseInput.DisableInput();
                OnCompleteOpen();
                return;
            }

            if (_toggleSeq != null && _toggleSeq.active)
                _toggleSeq.Kill();

            _baseInput.DisableInput();
            _toggleSeq = DOTween.Sequence();

            float strongShakePower = 3f;
            float shakeDuration = 0.3f;
            float weakShakePower = 1f;
            float shakeWaitDuration = 1f;
            float panelOpenPos = -5.2f;
            float lightOnDelay = 1.3f;

            _toggleSeq.AppendCallback(() => _cameraShakController.Shake(strongShakePower, shakeDuration))
                .AppendInterval(shakeWaitDuration)
                .AppendCallback(() => _cameraShakController.Shake(weakShakePower, _duration))
                .Append(transform.DOMoveY(panelOpenPos, _duration))
                .AppendCallback(() => _cameraShakController.Shake(strongShakePower, shakeDuration))
                .AppendInterval(shakeDuration)
                .AppendCallback(OnCompleteOpen)
                .AppendInterval(lightOnDelay)
                .AppendCallback(() => _ligthParent.SetActive(true));
        }

        public void Close()
        {
            if (_toggleSeq != null && _toggleSeq.active)
                _toggleSeq.Kill();

            _baseInput.EnableInput();
            _ligthParent.SetActive(false);
            foreach (var slot in _slots) slot.SetEnable(false);

            _cameraFocus.ResetFocus();
            //CameraManager.Instance.ResetFollow();
            //CameraManager.Instance.GetCompo<CameraZoomController>().ResetZoomLevel(1);

            // _toggleSeq = transform.DOMoveY(-12.5f, _duration);
        }

        private void OnCompleteOpen()
        {
            foreach (var slot in _slots) slot.SetEnable(true);

            _isOpen = true;
            _escCloser.SetControl(true);

            _cameraFocus.SetFocus();
            //CameraManager.Instance.SetFollow(_cameraFollow);
            //CameraManager.Instance.GetCompo<CameraZoomController>().SetZoomLevel(zoomInValue, zoomDuration);
        }


        private void HandlePurchaseWeapon()
        {
            if (_weaponInventory.IsUnlocked(_currentSelectedWeaponSO.id)) return;
            if (!_creditStorage.UseCredit(_currentSelectedWeaponSO.initialPrice)) return;

            _weaponInventory.UnlockWeapon(_currentSelectedWeaponSO.id);
            _currentSelectedSlot.SetActive(true);
            HandleSlotSelected(_currentSelectedWeaponSO, null); // debug
        }

        private void HandleSlotSelected(SubWeaponSO weaponSO, WeaponSlot slot)
        {
            if (weaponSO == null) return;
            _currentSelectedWeaponSO = weaponSO;
            _currentSelectedSlot = slot;

            if (_weaponInventory.IsUnlocked(weaponSO.id))
            {
                _weaponInfoPanel.SetWeaponData(weaponSO, _debugData);
                _weaponInfoPanel.Open();
                _purchasePanel.Close();
            }
            else
            {
                _weaponInfoPanel.Close();
                _purchasePanel.Open();
                _purchasePanel.SetPurchaseData(weaponSO, _creditStorage.CurrentCreditAmount, slot.transform.position);
            }
        }
    }

}
