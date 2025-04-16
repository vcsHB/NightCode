using UnityEngine;
namespace Office.Armory
{

    public class CreditStorage : MonoBehaviour
    {
        [SerializeField] private int _currentCredit;

        public int CurrentCreditAmount => _currentCredit;


        public bool IsEnough(int amount) => _currentCredit >= amount;

        /// <summary>
        /// Use Credits by amount
        /// </summary>
        /// <param name="amount">amount to use</param>
        /// <returns>is available</returns>
        public bool UseCredit(int amount)
        {
            if (IsEnough(amount))
            {
                _currentCredit -= amount;
                return true;
            }

            return false;
        }
    }
}