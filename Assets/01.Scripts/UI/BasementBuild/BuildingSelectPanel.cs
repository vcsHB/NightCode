using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class BuildingSelectPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _selectButtonPf;
        [SerializeField] private Transform _buttonParent;
        private BasementRoomType _selectedType;
        private RectTransform _rectTrm => transform as RectTransform;

        public BasementRoomType SelectedType => _selectedType;

        private void Awake()
        {
            foreach (BasementRoomType type in Enum.GetValues(typeof(BasementRoomType)))
            {
                if ((int)type < 3) continue;

                Button selectButton = Instantiate(_selectButtonPf, _buttonParent).GetComponent<Button>();
                selectButton.GetComponentInChildren<TextMeshProUGUI>().SetText(type.ToString());
                selectButton.onClick.AddListener(() =>
                {
                    _selectedType = type;
                    gameObject.SetActive(false);
                });
            }

            _rectTrm.sizeDelta 
                = new Vector2(_rectTrm.sizeDelta.x, 30 + 100 * _buttonParent.childCount);
        }

        public void ResetSelect() 
            => _selectedType = BasementRoomType.Empty;
    }
}
