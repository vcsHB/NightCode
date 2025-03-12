using Basement.Training;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Basement
{
    public class Cafe : BasementRoom
    {
        private CharacterEnum _positionedCharacter;
        private CafeUI _cafeUI;
        private Queue<Customer> _lineUpCustomers;
        private List<Customer> _exsistCustomers;
        private float _customerLineUpTime;
        private Customer _currentCustomer;
        private bool _isCustomerLinedUp = false;
        private List<Table> _tableList;


        public bool isCafeOpen = false;
        public BasementTime cafeOpenTime;
        public Furniture counterFurniture;
        public Transform currentLineTrm;

        public CharacterEnum PositionedCharacter
        {
            get
            {
                return _positionedCharacter;
            }
            set
            {
                _positionedCharacter = value;
                profitRange
                    = CharacterManager.Instance.GetCharacterExpectationProfit(_positionedCharacter);
            }
        }

        public Vector2 profitRange;
        public int totalProfit = 0;

        protected override void Start()
        {
            base.Start();
            _cafeUI = UIManager.Instance.GetUIPanel(BasementRoomType.Cafe) as CafeUI;
            _exsistCustomers = new List<Customer>();
            _lineUpCustomers = new Queue<Customer>();

            _tableList = GetComponentsInChildren<Table>().ToList();
        }

        private void OnDisable()
        {
            counterFurniture.InteractAction -= OpenCafeUI;
        }

        private Table FindEmptyTable()
            => _tableList.Find(table => table.IsCustomerExsist());

        private void Update()
        {
            if (_isCustomerLinedUp == false) return;

            //TODO: Change require time to follow selected Character's stat
            float requireTime = 3f; //3초에 1명씩 처리하는 씹 ACE

            if (_currentCustomer == null)
            {
                bool customerExsist = _lineUpCustomers.TryDequeue(out _currentCustomer);
                _customerLineUpTime = Time.time;

                _isCustomerLinedUp = customerExsist;
            }
            else if(_customerLineUpTime + requireTime < Time.time)
            {
                _currentCustomer.SetTable(FindEmptyTable());
                _currentCustomer = null;
            }
        }

        public override void Init(BasementController basement)
        {
            base.Init(basement);
            counterFurniture.Init(this);
            counterFurniture.InteractAction += OpenCafeUI;
        }

        private void OpenCafeUI()
        {
            _cafeUI.Init(this);
            _cafeUI.Open();
        }

        public void PassTime(int time)
        {
            if (isCafeOpen == false) return;

            //일단 30분 마다 30% 확률로 등장하는걸로?
            bool isCustomerEnter = false;
            int totalCustomer = 0;
            int totalCosts = 0;
            int totalTips = 0;

            for (int i = 0; i < time / 30; i++)
            {
                if (Random.Range(0, 100) < 30)
                {
                    isCustomerEnter = true;

                    int cost = Random.Range(2, 6);
                    int tip = Random.Range(0, (int)(cost * 0.3f));

                    totalCustomer++;
                    totalCosts += cost;
                    totalTips += tip;
                }
            }
            if (isCustomerEnter == false) return;

            string text = $"{totalCustomer}명 방문\n수익: {totalCosts}{(totalTips > 0 ? $"+TIP{totalTips}" : "")}";
            UIManager.Instance.msgText.PopMSGText(PositionedCharacter, text);
            //재화 추가해주기
        }

        public void EnterCustomer(Customer customer)
        {
            customer.Init(this);
            customer.SetDestination(currentLineTrm);
            _exsistCustomers.Add(customer);
        }

        public void LineUpCustomer(Customer customer)
        {
            _isCustomerLinedUp = true;
            _lineUpCustomers.Enqueue(customer);
            currentLineTrm.position += Vector3.right * customer.customerInfo.width;
        }

        public override void CloseUI()
        {
            _cafeUI.Close();
        }
        public override void OpenUI()
        {
            _roomUI.SetRoom(this);
            _roomUI.Open();
        }
    }
}
