using DG.Tweening;
using System.Collections.Generic;
using UI;
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

        private float _easingDuration = 0.2f;
        private List<float> _slotPositions = new List<float>();
        private List<MissionSelectButton> _missionButtonList = new List<MissionSelectButton>();
        private MissionSelectButton _selectedButton;

        private Sequence _seq;
        private Vector2 screenPosition = new Vector2(Screen.width, Screen.height);

        protected override void Awake()
        {
            base.Awake();
            scrollViewRect.anchoredPosition = new Vector2(0, screenPosition.y);
        }

        private void Update()
        {
            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                OfficeManager.Instance.ClearMission(missions[0]);
            }
        }

        public override void OpenAnimation()
        {
            if (_seq != null && _seq.active)
                _seq.Kill();

            SetInteractable(true);
            _seq = DOTween.Sequence();

            _seq.Append(_canvasGroup.DOFade(1, _easingDuration))
                .Append(scrollViewRect.DOAnchorPosY(0, _easingDuration).SetEase(Ease.OutCubic))
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
        }

        public override void CloseAnimation()
        {
            if (_seq != null && _seq.active)
                _seq.Kill();

            SetInteractable(false);
            _seq = DOTween.Sequence();

            _seq.Append(scrollViewRect.DOAnchorPosY(screenPosition.y, _easingDuration))
                .Append(_canvasGroup.DOFade(0, _easingDuration))
                .OnComplete(OnCompleteOpen);
        }

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
            _seq.Append(scrollViewRect.DOAnchorPosY(screenPosition.y, _easingDuration))
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

        public void Toggle()
        {
            if (isOpened) Close();
        }

        #region Mission

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

            SceneManager.LoadScene(_selectedButton.Mission.sceneName);
        }

        #endregion

    }
}
