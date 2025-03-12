using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Basement
{
    public class RoomUI : MonoBehaviour
    {
        public Button returnBtn;
        [SerializeField] private RoomInfoUI _roomInfoUI;
        [SerializeField] private RectTransform _buttonTrm;
        
        private BasementRoom _roomInfo;
        private Tween _tween;

        public void SetRoom(BasementRoom room)
        {
            _roomInfo = room;
            returnBtn.onClick.AddListener(Close);
        }

        public void OnClickRoomInfoUI()
        {
            _roomInfoUI.SetRoomSO(_roomInfo.roomSO);
            _roomInfoUI.Open();
        }

        public void Open()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _buttonTrm.DOAnchorPosX(85, 0.2f);
        }

        public void Close()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _buttonTrm.DOAnchorPosX(-85, 0.2f);
            _roomInfo?.ReturnFocus();
            _roomInfoUI?.Close();

            returnBtn.onClick.RemoveAllListeners();
        }
    }
}
