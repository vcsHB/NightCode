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
        private LodgingUI _lodgingUI;

        public CharacterEnum characterEnum => _character;
        public bool IsCharacterPlaced => _isCharacterPlaced;

        protected override void Awake()
        {
            base.Awake();
            _lodgingUI = UIManager.Instance.GetUIPanel(BasementRoomType.Lodging) as LodgingUI;
        }

        public int GetFatigueDecreaseValue(int restTime)
        {
            int decrease = _fatigueDecreasePerHalfHour * (restTime / 30);
            return decrease;
        }

        public int GetMaxRestTime()
            => Mathf.Clamp(30, (CharacterManager.Instance.GetFatigue(_character) / _fatigueDecreasePerHalfHour) * 30, 6000);

        public override void FocusRoom()
        {
            base.FocusRoom();
            _lodgingUI.Init(this);
            _lodgingUI.Open();
            UIManager.Instance.basementUI.Close();
        }

        public void SetCharacter(CharacterEnum character)
        {
            this._character = character;
            _isCharacterPlaced = true;
        }

        public void CancelplaceCharacter()
            => _isCharacterPlaced = false;

        public override void CloseUI()
        {
            _lodgingUI.Close();
            UIManager.Instance.basementUI.Open();
        }
    }
}
