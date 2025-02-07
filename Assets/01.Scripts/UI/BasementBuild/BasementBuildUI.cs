using UnityEngine;

namespace Basement
{
    public class BasementBuildUI : MonoBehaviour, IUIPanel
    {
        [SerializeField] private GameObject _buildUIObj;
        [SerializeField] private Toggle toggle;

        private void Awake()
        {
            toggle.onValueChange.AddListener(ToggleValueChange);
        }

        private void ToggleValueChange(bool isOpen)
        {
            if (isOpen)
                Open(Vector2.zero);
            else
                Close();
        }

        public void Open(Vector2 position)
        {
            _buildUIObj.SetActive(true);
        }

        public void Close()
        {
            _buildUIObj.SetActive(false);
        }
    }
}
