using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Office
{
    public class MissionSelectPanel : OfficeUIParent
    {
        public List<MissionSO> missions;
        public MissionSelectButton button;
        public MissionSlot missionSlotPf;
        public RectTransform bottomBarTrm;
        public Transform missionSlotParnet;
        public CharacterFormation formation;

        private float _easingDuration = 0.3f;
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
                MissionSlot missionSlot = Instantiate(missionSlotPf, missionSlotParnet);
                missionSlot.SetMission(this, mission);

                missionButton.RectTrm.anchoredPosition = new Vector2(-1250, position.y);
                missionButton.RectTrm.SetAsFirstSibling();
                missionButton.Init(mission);
                position += _offset;

                _selectButtons.Add(missionButton);
                _position.Add(position);
            });
        }

        public override void CloseAnimation()
        {
            if (_seq != null && _seq.active)
                _seq.Kill();

            _seq = DOTween.Sequence();
            _seq.Append(bottomBarTrm.DOAnchorPosY(-50f, _easingDuration))
                .OnComplete(OnCompleteClose);

            float insertTime = 0;
            for (int i = _selectButtons.Count - 1; i >= 0; i--)
            {
                _seq.Insert(insertTime, _selectButtons[i].RectTrm.DOAnchorPosX(-1250, _easingDuration));
                insertTime += 0.1f;
            }
        }

        public override void OpenAnimation()
        {
            if (_seq != null && _seq.active)
                _seq.Kill();

            _seq = DOTween.Sequence();

            _seq.Append(bottomBarTrm.DOAnchorPosY(50f, _easingDuration))
                .OnComplete(OnCompleteOpen);

            float insertTime = 0f;
            for (int i = _selectButtons.Count - 1; i >= 0; i--)
            {
                _seq.Insert(insertTime, _selectButtons[i].RectTrm.DOAnchorPosX(_position[i].x, _easingDuration));
                insertTime += 0.1f;
            }
        }

        public void SelectPanel(MissionSO mission)
        {
            MissionSelectButton panel = _selectButtons.Find(btn => btn.Mission == mission);
            SelectPanel(panel);
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
                    _seq.Insert(0, _selectButtons[i].RectTrm.DOAnchorPosX(-500f, _easingDuration))
                        .Join(_selectButtons[i].RectTrm.DORotate(Vector3.zero, _easingDuration));
                    _selectButtons[i].SelectButton();
                }
                else
                {

                    _seq.Insert(insertTime, _selectButtons[i].RectTrm.DOAnchorPosX(_position[i].x + 500f, _easingDuration))
                        .Join(_selectButtons[i].RectTrm.DORotate(new Vector3(0, 0, 5), _easingDuration));
                    _selectButtons[i].UnSelectButton();

                    insertTime += 0.1f;
                }
            }
        }
    }
}
