using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    public class CafeTable : MonoBehaviour
    {
        public Transform customerPosition;

        public ProcessInputObject clickProcess;
        public Image iconRenderer;
        public Image patientFill;
        public Sprite serveIcon, cleanIcon;

        private CafePlayer _player;
        private Collider2D _collider;
        private bool _isCustomerWaitingMenu = false;
        private float _currentWaitingTime;
        private float _customerPatientTime;

        public CafeCustomer AssingedCustomer { get; private set; }
        public bool IsClean { get; private set; } = true;
        public bool IsCustomerExsist { get => AssingedCustomer != null; }
        public float WaitingTime => _currentWaitingTime;

        private void Awake()
        {
            IsClean = true;
            _collider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (_isCustomerWaitingMenu)
            {
                _currentWaitingTime += Time.deltaTime;
                patientFill.fillAmount = _currentWaitingTime / _customerPatientTime;

                if(_currentWaitingTime >= _customerPatientTime)
                {
                    Debug.Log("나는 화가난!!");
                }
            }
        }

        //데이터상 손님 할당
        public void SetCustomer(CafeCustomer customer)
        {
            AssingedCustomer = customer;
            _customerPatientTime = customer.customerSO.menuWaitingTime;
        }

        //게임에서 손님 할당
        public void CustomerSit()
        {
            _collider.enabled = true;
            iconRenderer.gameObject.SetActive(true);
            iconRenderer.sprite = serveIcon;

            _isCustomerWaitingMenu = true;
            _currentWaitingTime = 0;
        }

        //메뉴를 서빙해 줄 때
        public void OnServingMenu()
        {
            _collider.enabled = false;
            iconRenderer.gameObject.SetActive(false);
            AssingedCustomer.GetFood();
            _isCustomerWaitingMenu = false;
            patientFill.fillAmount = 0;
        }

        //손님이 떠날때
        public void LeaveCustomer()
        {
            if (AssingedCustomer == null) return;

            AssingedCustomer = null;
            IsClean = false;

            _collider.enabled = true;
            iconRenderer.gameObject.SetActive(true);
            iconRenderer.sprite = cleanIcon;
            patientFill.fillAmount = 0;
        }


        public void CleanTable()
        {
            _collider.enabled = false;
            iconRenderer.gameObject.SetActive(false);
            IsClean = true;
        }


        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out _player) == false) return;

            if (_isCustomerWaitingMenu)
            {
                if (_player.isGetFood == false) return;

                OnServingMenu();
                _player.ServeFood();
                return;
            }

            if (IsClean || _player.isGetFood) return;

            clickProcess.Init(this);
        }

        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (_isCustomerWaitingMenu || IsClean) return;
            if (collision.TryGetComponent(out _player) == false) return;

            clickProcess.Close();
        }


        public bool CanCustomerSitdown() => (IsClean && !IsCustomerExsist);
    }
}
