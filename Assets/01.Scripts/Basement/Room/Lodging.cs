using Basement;
using Basement.Training;
using Unity.VisualScripting;
using UnityEngine;

namespace Basement
{
    public class Lodging : BasementRoom
    {
        private int _fatigueDecreasePerHalfHour = 2;
        private CharacterEnum _character;
        private bool _isCharacterPlaced = false;

        public CharacterEnum characterEnum => _character;
        public bool IsCharacterPlaced => _isCharacterPlaced;

        public int GetFatigueDecreaseValue(int restTime)
        {
            int decrease = _fatigueDecreasePerHalfHour * (restTime / 30);
            return decrease;
        }

        public int GetMaxRestTime()
            => Mathf.Clamp(30, (CharacterManager.Instance.GetFatigue(_character) / _fatigueDecreasePerHalfHour) * 30,6000);

        public override void FocusRoom()
        {
            base.FocusRoom();
            UIManager.Instance.lodgingUI.Init(this);
            UIManager.Instance.lodgingUI.Open();
        }

        public void SetCharacter(CharacterEnum character)
        {
            this._character = character;
            _isCharacterPlaced = true;
        }

        public void CancelplaceCharacter()
            => _isCharacterPlaced = false;
    }
}
