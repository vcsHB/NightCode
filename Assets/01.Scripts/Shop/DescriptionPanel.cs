using TMPro;
using UnityEngine;
namespace Shop
{

    public class DescriptionPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _nameText;
        [SerializeField] private TextMeshPro _descriptionText;

        public void SetContent(string name, string description)
        {
            _nameText.text = name;
            _descriptionText.text = description;

        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false); // 임시
        }
    }
}