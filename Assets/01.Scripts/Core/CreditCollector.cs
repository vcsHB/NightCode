using TMPro;
using UnityEngine;

namespace Core.DataControl
{
    public class CreditCollector : MonoSingleton<CreditCollector>
    {
        public int multiplier = 1;
        public TextMeshProUGUI creditText;

        private void Start()
        {
            creditText.SetText($"{DataLoader.Instance.Credit}");
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.C))
            {
                CollectCredit(100);
            }
        }

        public void CollectCredit(int amount)
        {
            DataLoader.Instance.AddCredit(amount * multiplier);
            creditText.SetText($"{DataLoader.Instance.Credit}");
        }

        public void UseCredit(int amount)
        {
            DataLoader.Instance.AddCredit(-amount);
            creditText.SetText($"{DataLoader.Instance.Credit}");
        }
    }
}
