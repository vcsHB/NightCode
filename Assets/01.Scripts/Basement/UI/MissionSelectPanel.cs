using DG.Tweening;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Basement.Mission
{
    public class MissionSelectPanel : BasementUI
    {
        public List<MissionSO> missions;
        public MissionSelectButton button;

        [SerializeField] private Vector2 _offset;
        private List<MissionSelectButton> _selectButtons;
        private List<Vector2> _position;
        private Sequence _seq;

        private void Awake()
        {
            _selectButtons = new List<MissionSelectButton>();
            _position = new List<Vector2>();
            Vector2 position = Vector2.zero;

            missions.ForEach(mission =>
            {
                MissionSelectButton missionButton = Instantiate(button, transform);
                missionButton.RectTrm.anchoredPosition = new Vector2(-1250, position.y);
                missionButton.RectTrm.SetAsFirstSibling();
                missionButton.Init(mission);
                position += _offset;

                _selectButtons.Add(missionButton);
                _position.Add(position);
            });
        }

        protected override void CloseAnimation()
        {
            if (_seq != null && _seq.active)
                _seq.Kill();

            _seq = DOTween.Sequence();
            _seq.OnComplete(OnCompleteCloseAction);

            float insertTime = 0;
            for (int i = _selectButtons.Count - 1; i >= 0; i--)
            {
                _seq.Insert(insertTime, _selectButtons[i].RectTrm.DOAnchorPosX(-1250, 0.3f));
                insertTime += 0.1f;
            }
        }

        protected override void OpenAnimation()
        {
            if (_seq != null && _seq.active)
                _seq.Kill();

            _seq = DOTween.Sequence();
            _seq.OnComplete(OnCompleteOpenAction);

            float insertTime = 0f;
            for (int i = _selectButtons.Count - 1; i >= 0; i--)
            {
                _seq.Insert(insertTime, _selectButtons[i].RectTrm.DOAnchorPosX(_position[i].x, 0.3f));
                insertTime += 0.1f;
            }
        }

        public void SelectPanel(MissionSelectButton missionSelectButton)
        {
            if (_seq != null && _seq.active)
                _seq.Kill();

            _seq = DOTween.Sequence();
            float insertTime = 0.1f;

            for (int i = _selectButtons.Count - 1; i >= 0; i--)
            {
                if (_selectButtons[i] == missionSelectButton)
                {
                    _seq.Insert(0, _selectButtons[i].RectTrm.DOAnchorPosX(-500f, 0.3f))
                        .Join(_selectButtons[i].RectTrm.DORotate(Vector3.zero, 0.3f));
                }
                else
                {
                    _seq.Insert(insertTime, _selectButtons[i].RectTrm.DOAnchorPosX(_position[i].x + 500f, 0.3f))
                        .Join(_selectButtons[i].RectTrm.DORotate(new Vector3(0, 0, 5), 0.3f));
                    _selectButtons[i].UnSelectButton();

                    insertTime += 0.1f;
                }
            }
        }
    }
}
