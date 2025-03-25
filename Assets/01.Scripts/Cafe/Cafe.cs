using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cafe
{
    public class Cafe : MonoBehaviour
    {
        public Transform customerInitPosition;
        public List<CafeCustomer> customerList;
        public Transform tableParent;

        private List<CafeTable> tableList;

        private void Awake()
        {
            tableList = new List<CafeTable>();
            for (int i = 0; i < tableParent.childCount; i++)
            {
                if (tableParent.GetChild(i).TryGetComponent(out CafeTable table))
                    tableList.Add(table);
            }
        }


        public bool EnterCustomer(CafeCustomerSO customerSO)
        {
            if (TryGetValiadeTable(out CafeTable table))
            {
                CafeCustomer customer = Instantiate(customerSO.customerPf, customerInitPosition);
                customer.Init(table);
                return true;
            }
            return false;
        }

        public bool TryGetValiadeTable(out CafeTable table)
        {
            CafeTable valiadeTable = null;

            tableList.ForEach(t =>
            {
                if (t.CanCustomerSitdown())
                {
                    valiadeTable = t;
                    return;
                }
            });

            table = valiadeTable;
            return (valiadeTable != null);
        }
    }
}
