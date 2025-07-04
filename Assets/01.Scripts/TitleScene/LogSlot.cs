using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace TitleScene
{

    public class LogSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _contentText;
        [SerializeField] private Image _symbolImage;

        public void SetContent(LogContent content)
        {
            _contentText.text = content.content;
            _contentText.color = content.color;
            _symbolImage.color = content.color;
            _symbolImage.sprite = content.symbol;
        }
    }
}