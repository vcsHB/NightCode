using System.Linq;
using UnityEngine;
using Basement.NPC;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Basement
{
    public class Cafe : BasementRoom
    {
        private List<Table> _tableList;

        private Queue<Employee> _employeeQueue;

        //아직 가게 들어오기 전
        private Queue<Customer> _lineUpCustomers;
        // 테이블에 앉은 상태
        private Queue<Customer> _menuWaitingCustomers;

        public bool isCafeOpen = false;
        public BasementTime cafeOpenTime;
        public Furniture counterFurniture;

        public Transform employeeParent;
        public Transform customerParent;
        public Transform employeePosition;
        public Transform exit;
        public CharacterEnum PositionedCharacter;

        //디버깅용
        [SerializeField] private CustomerSO debugCustomer;
        [SerializeField] private CafeNPC npc;
        [SerializeField] private List<EmployeeSO> employeeList;

        private CafeUI _cafeUI;
        public CafeUI CafeUI
        {
            get
            {
                if (_cafeUI == null)
                    _cafeUI = UIManager.Instance.GetUIPanel(BasementRoomType.Cafe) as CafeUI;
                return _cafeUI;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            npc.Init(this);
            _lineUpCustomers = new Queue<Customer>();
            _menuWaitingCustomers = new Queue<Customer>();
            _employeeQueue = new Queue<Employee>();

            _tableList = GetComponentsInChildren<Table>().ToList();

            employeeList.ForEach(employee =>
            {
                Employee employeeInstance = Instantiate(employee.employeePf, employeeParent);
                employeeInstance.gameObject.SetActive(false);
                _employeeQueue.Enqueue(employeeInstance);
            });
        }

        private void OnDisable()
        {
            counterFurniture.InteractAction -= OpenCafeUI;
        }

        private void Update()
        {
            //FOR DEBUGING
            if (Keyboard.current.cKey.wasPressedThisFrame)
                AddCustomer(debugCustomer);

            if(Keyboard.current.vKey.wasPressedThisFrame)
            {
                Debug.Log($"직원 수: {_employeeQueue.Count} 고객 수{_menuWaitingCustomers.Count}");
            }
        }

        /// <summary>
        /// 서빙하러 움직이기 시작
        /// </summary>
        private void TryServeMenu()
        {
            while(_menuWaitingCustomers.TryPeek(out Customer customer))
            {
                if (_employeeQueue.TryDequeue(out Employee employee))
                {
                    //직원 출동
                    employee.transform.position = employeePosition.position;
                    employee.Init(this, customer.TargetTable);
                    _menuWaitingCustomers.Dequeue();
                }
                else break;
            }

            //현재 직원이 있고
            //if (_employeeQueue.TryDequeue(out Employee employee))
            //{
            //    //메뉴를 기다리는 고객이 있다면
            //    if (_menuWaitingCustomers.TryDequeue(out Customer customer))
            //    {
            //        Debug.Log(_menuWaitingCustomers.Count);
            //        employee.transform.position = employeePosition.position;
            //        employee.Init(this, customer.TargetTable);
            //    }
            //}
        }

        private Table FindEmptyTable()
            => _tableList.Find(table => table.IsCustomerExsist() == false);

        private void AddCustomer(CustomerSO customerSO)
        {
            Customer customer = Instantiate(customerSO.customerPf, customerParent);
            EnterCustomer(customer);
        }

        private void EnterCustomer(Customer customer)
        {
            Table emptyTable = FindEmptyTable();

            if (emptyTable != null)
            {
                customer.gameObject.SetActive(true);
                customer.SetTable(emptyTable);
                customer.Init(this);
                customer.transform.position = exit.position;
            }
            else
            {
                //자리가 없으면 데이터 상에서만 줄을 세우고
                customer.gameObject.SetActive(false);
                _lineUpCustomers.Enqueue(customer);
            }
        }

        /// <summary>
        /// 손님이 테이블에 앉았을 때
        /// </summary>
        /// <param name="customer"></param>
        public void OnCustomerSitTable(Customer customer)
        {
            _menuWaitingCustomers.Enqueue(customer);
            TryServeMenu();
        }

        /// <summary>
        /// 손님이 테이블을 떠날 때
        /// </summary>
        /// <param name="employee"></param>
        public void ReturnEmployee(Employee employee)
        {
            employee.gameObject.SetActive(false);
            _employeeQueue.Enqueue(employee);
        }

        // 손님이 떠나면 테이블이 비었다는 뜻임으로
        // 다시 서빙할 애들이 있는지 확인
        public void OnLeaveCustomer(Customer customer)
        {
            TryServeMenu();
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

        private void OpenCafeUI()
        {
            CafeUI.Init(this);
            CafeUI.Open();
            //UIManager.Instance.returnButton.ChangeReturnAction(CafeUI.Close);
            UIManager.Instance.basementUI.Close();
            RoomUI.Close();
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
    }
}
