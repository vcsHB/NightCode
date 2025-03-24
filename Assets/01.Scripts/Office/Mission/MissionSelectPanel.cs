using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Office
{
    public class MissionSelectPanel : OfficeUIParent
    {
        public List<MissionSO> missions;
        public MissionSelectButton button;

        [Space]
        public MissionSlot missionSlotPf;
        public RectTransform bottomBarTrm;
        public Transform missionSlotParnet;

        [Space]
        public CharacterFormation formation;

        [Space]
        public RectTransform scrollViewRect;
        public RectTransform contentRect;

        [Space]
        [SerializeField] private float _leftPedding;
        [SerializeField] private float _space;

        private float _easingDuration = 0.4f;
        private List<float> _slotPositions = new List<float>();
        private List<MissionSelectButton> _missionButtonList = new List<MissionSelectButton>();
        private MissionSelectButton _selectedButton;

        private Sequence _seq;

        private void Update()
        {
            if (Keyboard.current.lKey.wasPressedThisFrame)
                OfficeManager.Instance.ClearMission(missions[0]);
        }

        public override void OpenAnimation()
        {
            if (_seq != null && _seq.active)
                _seq.Kill();

            _seq = DOTween.Sequence();

            _seq.Append(scrollViewRect.DOAnchorPosX(0, _easingDuration).SetEase(Ease.OutCubic))
                .OnUpdate(() => contentRect.anchoredPosition = new Vector2(-contentRect.rect.width, 0))
                .OnComplete(() =>
                {
                    OnCompleteOpen();
                    if (_selectedButton != null)
                    {
                        _selectedButton = null;
                    }
                });

            if (_selectedButton != null)
            {
                float position = _slotPositions[_missionButtonList.IndexOf(_selectedButton)];
                _seq.AppendCallback(() => _selectedButton.RectTrm.SetParent(contentRect));
                _seq.Append(_selectedButton.RectTrm.DOAnchorPosX(position, _easingDuration));
            }

            //for (int i = 0; i < _missionButtonList.Count; i++)
            //{
            //    RectTransform btnTrm = _missionButtonList[i].RectTrm;

            //    if (btnTrm.parent == transform)
            //    {
            //        btnTrm.SetParent(contentRect);
            //        float targetPosition = btnTrm.localPosition.x;
            //        btnTrm.SetParent(transform);

            //        _seq.Append(btnTrm.DOAnchorPosX(targetPosition, _easingDuration));
            //    }
            //}
        }

        public override void CloseAnimation()
        {
            if (_seq != null && _seq.active)
                _seq.Kill();

            _seq = DOTween.Sequence();

            _seq.Append(scrollViewRect.DOAnchorPosX(-1920, _easingDuration))
                .OnComplete(OnCompleteOpen);
        }


        //-720, 0
        public void SelectPanel(MissionSO mission)
        {
            MissionSelectButton panel = _missionButtonList.Find(btn => btn.Mission == mission);
            SelectPanel(panel);
        }

        public void SelectPanel(MissionSelectButton missionSelectButton)
        {
            if (_seq != null && _seq.active)
                _seq.Kill();

            missionSelectButton.transform.SetParent(transform);
            _selectedButton = missionSelectButton;

            _seq = DOTween.Sequence();
            _seq.Append(scrollViewRect.DOAnchorPosX(-1920, _easingDuration))
                .Join(missionSelectButton.RectTrm.DOAnchorPosX(-500, _easingDuration))
                .OnComplete(formation.Open);

            formation.onCloseUI -= Open;
            formation.onCloseUI += Open;
        }

        public override void CloseAllUI()
        {
            formation.onCloseUI -= Open;
            base.CloseAllUI();
        }



        public void AddMission(MissionSO mission)
        {
            missions.Add(mission);

            MissionSelectButton missionButton = Instantiate(button, contentRect);
            MissionSlot missionSlot = Instantiate(missionSlotPf, missionSlotParnet);
            missionSlot.SetMission(this, mission);

            float width = missionButton.RectTrm.rect.width;
            float position = _leftPedding + (width / 2) + (_slotPositions.Count * (_leftPedding + width));

            missionButton.RectTrm.anchoredPosition = new Vector2(position, 0);
            missionButton.RectTrm.SetAsFirstSibling();
            missionButton.Init(mission);
            _missionButtonList.Add(missionButton);
            _slotPositions.Add(position);
        }

        public void RemoveMission(MissionSO mission)
        {
            int index = missions.IndexOf(mission);
            Destroy(_missionButtonList[index].gameObject);

            missions.RemoveAt(index);
            _slotPositions.RemoveAt(index);
            _missionButtonList.RemoveAt(index);
        }


        public void EnterMission()
        {
            CharacterEnum[] characterFormation = new CharacterEnum[3];
            for (int i = 0; i < 3; i++)
            {
                characterFormation[i] = formation.GetCharacter(i);
                Debug.Log(characterFormation[i]);
            }

            Debug.Log(_selectedButton.Mission.sceneName);
            //SceneManager.LoadScene(_selectedButton.Mission.sceneName);
        }
    }
}
