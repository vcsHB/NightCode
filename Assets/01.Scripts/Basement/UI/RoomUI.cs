using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class RoomUI : MonoBehaviour
    {
        [SerializeField] private RoomInfoUI _roomInfoUI;
        [SerializeField] private RectTransform _buttonTrm;
        [SerializeField] private GameObject _returnBtn;
        private BasementRoom _roomInfo;
        private Tween _tween;

        public void SetRoom(BasementRoom room)
        {
            _roomInfo = room;


        }

        public void OnClickRoomInfoUI()
        {
            _roomInfoUI.SetRoomSO(_roomInfo.roomSO);
            _roomInfoUI.Open();
        }

        public void Open()
        {
            if(_tween != null && _tween.active)
                _tween.Kill();

            _tween = _buttonTrm.DOAnchorPosX(85, 0.2f);
            _returnBtn.SetActive(true);
        }

        public void Close()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _buttonTrm.DOAnchorPosX(-85, 0.2f);
            _returnBtn.SetActive(false);
        }
    }
}
