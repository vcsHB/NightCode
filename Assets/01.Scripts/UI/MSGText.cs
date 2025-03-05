using UI;
using UnityEngine;

namespace Basement
{
    public class MSGText : MonoBehaviour
    {
        [SerializeField] private MSGTextBox _textBox;

        public void AddMSGText(Sprite icon, string text)
        {
            MSGTextBox textBox = Instantiate(_textBox, transform);
            textBox.Init(icon, text);
        }
    }
}
