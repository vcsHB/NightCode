using UnityEngine;

namespace Basement
{
    public class Table : Furniture
    {
        public Transform customerPositionTrm;
        public Transform employeePositionTrm;
        private Customer _enteredCustomer;
        private float _enterTime;

        public bool IsCustomerExsist() => _enteredCustomer != null;

        public void CustomerSitdown(Customer customer)
        {
            _enteredCustomer = customer;
            _enterTime = Time.time;
        }

        public void CustomerLeave()
        {
            _enteredCustomer = null;
        }
    }
}
