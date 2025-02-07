using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class BuildingSelectPanel : MonoBehaviour
    {
        public Action<BasementRoomType> onSelectRoom;

        [SerializeField] private GameObject _selectButtonPf;
        [SerializeField] private Transform _buttonParent;
        private RectTransform _rectTrm => transform as RectTransform;

        private void Awake()
        {
            foreach (BasementRoomType type in Enum.GetValues(typeof(BasementRoomType)))
            {
                if ((int)type < 3) continue;

                Button selectButton = Instantiate(_selectButtonPf, _buttonParent).GetComponent<Button>();
                selectButton.GetComponentInChildren<TextMeshProUGUI>().SetText(type.ToString());
                selectButton.onClick.AddListener(() =>
                {
                    onSelectRoom?.Invoke(type);
                    gameObject.SetActive(false);
                });
            }

            _rectTrm.sizeDelta 
                = new Vector2(_rectTrm.sizeDelta.x, 30 + 100 * _buttonParent.childCount);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }
    }
}
