using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Basement
{
    public class RoomInfoUI : MonoBehaviour
    {
        [SerializeField]private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _explainText;

        private Tween _tween;
        private BasementRoomSO _roomSO;

        private RectTransform _rctTrm;

        public void SetRoomSO(BasementRoomSO roomSO)
            => _roomSO = roomSO;

        public void Open()
        {
            if (_tween != null && _tween.active) 
                _tween.Kill();

            _tween = _rctTrm.DOAnchorPosX(0, 0.3f);
        }

        public void Close()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rctTrm.DOAnchorPosX(500, 0.3f);
        }
    }
}
