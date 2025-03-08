using Basement.Training;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public abstract class BasementRoomCharacterPlaceUI : MonoBehaviour
    {
        public Image icon;
        public TMP_Dropdown characterSelectDropDown;
        public CharacterIconSO iconSO;
        public List<GameObject> canNotSetCharacter;

        protected CharacterEnum _selectedCharacter;

        protected virtual void Awake()
        {
            characterSelectDropDown.onValueChanged.AddListener(OnSelectCharacter);
        }

        protected virtual void OnDisable()
        {
            characterSelectDropDown.onValueChanged.RemoveListener(OnSelectCharacter);
        }

        protected virtual void OnSelectCharacter(int value)
        {
            _selectedCharacter
                = Enum.Parse<CharacterEnum>(characterSelectDropDown.options[value].text);

            icon.sprite = iconSO.GetIcon(_selectedCharacter);
            characterSelectDropDown.value = value;

            bool isWorking = WorkManager.Instance.CheckWorking(_selectedCharacter);
            canNotSetCharacter.ForEach(obj => obj.SetActive(isWorking));
        }
    }
}
