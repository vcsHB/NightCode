using UnityEngine;

namespace Cafe
{
    public class CafeTable : MonoBehaviour
    {
        public Transform customerPosition;

        public ProcessInputObject clickProcess;
        public SpriteRenderer iconRenderer;
        public Sprite serveIcon, cleanIcon;

        private CafePlayer _player;
        private bool _isCustomerWaitingMenu = false;

        public CafeCustomer AssingedCustomer { get; private set; }
        public bool IsClean { get; private set; } = true;
        public bool IsCustomerExsist { get => AssingedCustomer != null; }


        private void Awake()
        {
            IsClean = true;
        }

        //데이터상 손님 할당
        public void SetCustomer(CafeCustomer customer)
        {
            AssingedCustomer = customer;
        }

        //게임에서 손님 할당
        public void CustomerSit()
        {
            iconRenderer.gameObject.SetActive(true);
            iconRenderer.sprite = serveIcon;
            _isCustomerWaitingMenu = true;
        }

        //메뉴를 서빙해 줄 때
        public void OnServingMenu()
        {
            iconRenderer.gameObject.SetActive(false);
            AssingedCustomer.OnGetFood();
            _isCustomerWaitingMenu = false;
        }

        //손님이 떠날때
        public void LeaveCustomer()
        {
            AssingedCustomer = null;
            IsClean = false;

            iconRenderer.gameObject.SetActive(true);
            iconRenderer.sprite = cleanIcon;
        }


        public void CleanTable()
        {
            clickProcess.onComplete -= CleanTable;
            iconRenderer.gameObject.SetActive(false);
            IsClean = true;
        }


        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out _player) == false) return;

            if(_isCustomerWaitingMenu)
            {
                if (_player.isGetFood == false) return;

                OnServingMenu();
                _player.ServeFood();
                return;
            }

            if (IsClean || _player.isGetFood) return;

            clickProcess.onComplete += CleanTable;
            clickProcess.Init(this);
        }

        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (_isCustomerWaitingMenu || IsClean) return;
            if (collision.TryGetComponent(out _player) == false) return;

            clickProcess.onComplete -= CleanTable;
            clickProcess.Close();
        }


        public bool CanCustomerSitdown() => (IsClean && !IsCustomerExsist);
    }
}
