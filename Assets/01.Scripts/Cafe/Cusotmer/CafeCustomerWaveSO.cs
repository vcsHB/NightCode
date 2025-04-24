using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cafe
{
    [CreateAssetMenu(menuName = "SO/Cafe/CustomerWave")]
    public class CafeCustomerWaveSO : ScriptableObject
    {
        public List<CustomerInfo> exsistCustomer;
    }

    [Serializable]
    public struct CustomerInfo
    {
        public CafeCustomerSO customer;
        public float exsistDelay;
    }
}
