using UnityEngine;

namespace Cafe
{
    public class CafeTable : MonoBehaviour
    {
        public bool isClean { get; private set; }
        

        public bool CanCustomerSitdown()
            => isClean;
    }
}
