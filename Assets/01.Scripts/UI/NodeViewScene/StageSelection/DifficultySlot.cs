using UnityEngine;
using UnityEngine.UI;
namespace UI.NodeViewScene.StageSelectionUIs
{

    public class DifficultySlot : MonoBehaviour
    {
        private Image _image;
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetColor(Color color)
        {
            _image.color = color;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}