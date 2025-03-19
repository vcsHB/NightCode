using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Basement
{
    public class RoomUI : BasementUI
    {
        [Space]
        [SerializeField] private RoomInfoUI _roomInfoUI;
        [SerializeField] private RectTransform _buttonTrm;
        
        private BasementRoom _roomInfo;
        private Tween _uiMoveTween;

        public void SetRoom(BasementRoom room)
        {
            _roomInfo = room;
            //UIManager.Instance.returnButton.AddReturnAction(Close);
        }

        public void OnClickRoomInfoUI()
        {
            _roomInfoUI.SetRoomSO(_roomInfo.roomSO);
            SetUILink(_roomInfoUI);
            _roomInfoUI.Open();
        }

        protected override void OpenAnimation()
        {

            if (_uiMoveTween != null && _uiMoveTween.active)
                _uiMoveTween.Kill();

            _uiMoveTween = _buttonTrm.DOAnchorPosX(85, 0.2f)
                .OnComplete(() => onCompleteOpen?.Invoke());
        }

        protected override void CloseAnimation()
        {
            _roomInfoUI?.Close();

            if (_uiMoveTween != null && _uiMoveTween.active)
                _uiMoveTween.Kill();

            _uiMoveTween = _buttonTrm.DOAnchorPosX(-85, 0.2f)
                .OnComplete(() => onCompleteClose?.Invoke());
        }

        public void ReturnToWideView()
        {
            BasementManager.Instance.basement.ReturnToWideView();
            Close();
        }
    }
}
