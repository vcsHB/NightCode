using DG.Tweening;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Basement.Mission
{
    public class MissionSelectPanel : MonoBehaviour, IWindowPanel
    {
        public List<MissionSO> missions;
        public MissionSelectButton button;

        private RectTransform _rectTrm => transform as RectTransform;
        private RectTransform _childRect => transform.GetChild(0) as RectTransform;

        private Tween _tween;

        private void Awake()
        {
            missions.ForEach(mission =>
            {
                MissionSelectButton missionButton = Instantiate(button, transform.GetChild(0));
                missionButton.Init(mission);
            });
        }

        public void Close()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rectTrm.DOAnchorPosX(0, 0.3f);
        }

        public void Open()
        {
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = _rectTrm.DOAnchorPosX(_childRect.rect.width, 0.3f);
        }
    }
}
