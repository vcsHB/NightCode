using System.Linq;
using UnityEngine;
using Basement.NPC;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Basement
{
    public class Cafe : BasementRoom
    {
        private Queue<Customer> _lineUpCustomers;
        private List<Customer> _exsistCustomers;
        private List<Table> _tableList;
        public Queue<Customer> menuWaitingCustomers;

        private bool _isCustomerLinedUp = false;
        private float _customerLineUpTime;
        private Customer _currentCustomer;

        public bool isCafeOpen = false;
        public BasementTime cafeOpenTime;
        public Furniture counterFurniture;
        public Transform currentLineTrm;
        public Transform customerParent;
        public Transform employeePosition;
        public Transform exit;
        public CharacterEnum PositionedCharacter;
        [SerializeField] private CustomerSO debugCustomer;
        [SerializeField] private CafeNPC npc;

        private CafeUI _cafeUI;
        public CafeUI CafeUI
        {
            get
            {
                if(_cafeUI == null)
                    _cafeUI = UIManager.Instance.GetUIPanel(BasementRoomType.Cafe) as CafeUI;
                return _cafeUI;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            npc.Init(this);
            _exsistCustomers = new List<Customer>();
            _lineUpCustomers = new Queue<Customer>();
            menuWaitingCustomers = new Queue<Customer>();

            _tableList = GetComponentsInChildren<Table>().ToList();
        }

        private void OnDisable()
        {
            counterFurniture.InteractAction -= OpenCafeUI;
        }

        private Table FindEmptyTable()
            => _tableList.Find(table => table.IsCustomerExsist() == false);

        private void Update()
        {
            //FOR DEBUGING
            if (Keyboard.current.cKey.wasPressedThisFrame)
                AddCustomer();

            if (_isCustomerLinedUp == false) return;

            //TODO: Change require time to follow selected Character's stat
            float requireTime = 3f; //3초에 1명씩 처리하는 씹 ACE

            if (_currentCustomer == null)
            {
                bool customerExsist = _lineUpCustomers.TryDequeue(out _currentCustomer);
                _customerLineUpTime = Time.time;

                _isCustomerLinedUp = customerExsist;
            }
            else if (_customerLineUpTime + requireTime < Time.time)
            {
                SetCustomerTable();
            }
        }

        private void OpenCafeUI()
        {
            CafeUI.Init(this);
            CafeUI.Open();
            UIManager.Instance.returnButton.ChangeReturnAction(CafeUI.Close);
            UIManager.Instance.basementUI.Close();
            RoomUI.Close();
        }

        private void SetCustomerTable()
        {
            Table emptyTable = FindEmptyTable();
            if (emptyTable == null || npc.stateMachine.currentStateString != "Counter")
                return;

            menuWaitingCustomers.Enqueue(_currentCustomer);

            _currentCustomer.SetTable(emptyTable);
            currentLineTrm.position += Vector3.left * _currentCustomer.customerInfo.width;

            Queue<Customer> temp = new Queue<Customer>();
            while (_lineUpCustomers.TryDequeue(out Customer customer))
            {
                customer.SetDestination(customer.Destination + (Vector2.left * _currentCustomer.customerInfo.width));
                temp.Enqueue(customer);
            }

            while (temp.TryDequeue(out Customer customer))
                _lineUpCustomers.Enqueue(customer);

            _currentCustomer = null;
        }


        private void AddCustomer()
        {
            Customer customer = Instantiate(debugCustomer.customerPf, customerParent);
            EnterCustomer(customer);
        }

        public void EnterCustomer(Customer customer)
        {
            customer.SetDestination(currentLineTrm.position);
            customer.Init(this);
            _exsistCustomers.Add(customer);
        }

        public void LineUpCustomer(Customer customer)
        {
            _isCustomerLinedUp = true;
            _lineUpCustomers.Enqueue(customer);
        }

        public void GetMoney(int money)
        {
            Vector2 position = Camera.main.WorldToScreenPoint(employeePosition.position + new Vector3(0, 1, 0));
            UIManager.Instance.SetPopupText($"<color=green>{money}$", position);
        }

        public override void CloseUI()
        {
            CafeUI.Close();
        }

        public override void Init(BasementController basement)
        {
            base.Init(basement);
            counterFurniture.Init(this);
            counterFurniture.InteractAction += OpenCafeUI;
        }


        public void PassTime(int time)
        {
            //if (isCafeOpen == false) return;

            //일단 30분 마다 30% 확률로 등장하는걸로?
            //bool isCustomerEnter = false;
            //int totalCustomer = 0;
            //int totalCosts = 0;
            //int totalTips = 0;

            //for (int i = 0; i < time / 30; i++)
            //{
            //    if (Random.Range(0, 100) < 30)
            //    {
            //        isCustomerEnter = true;

            //        int cost = Random.Range(2, 6);
            //        int tip = Random.Range(0, (int)(cost * 0.3f));

            //        totalCustomer++;
            //        totalCosts += cost;
            //        totalTips += tip;
            //    }
            //}
            //if (isCustomerEnter == false) return;

            //string text = $"{totalCustomer}명 방문\n수익: {totalCosts}{(totalTips > 0 ? $"+TIP{totalTips}" : "")}";
            //UIManager.Instance.msgText.PopMSGText(PositionedCharacter, text);
            ////재화 추가해주기
        }
    }
}
