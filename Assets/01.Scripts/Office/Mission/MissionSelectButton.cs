using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Office
{
    public class MissionSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _missionTypeText;
        [SerializeField] private TextMeshProUGUI _missionNameText;
        [SerializeField] private List<GameObject> _difficultyObjects;
        [SerializeField] private TextMeshProUGUI _rewardText;
        [SerializeField] private TextMeshProUGUI _explainText;
        [SerializeField] private Image _icon;

        public RectTransform RectTrm => transform as RectTransform;
        public RectTransform childRect => transform.GetChild(0) as RectTransform;
        public MissionSO Mission => _mission;

        private MissionSelectPanel _selectPanel;
        private bool _isSelected = false;
        private MissionSO _mission;
        private Tween _tween;



        public void Init(MissionSO mission)
        {
            _mission = mission;
            _missionTypeText.SetText(_mission.missionType.ToString());
            _missionNameText.SetText(_mission.missionName);
            _explainText.SetText(_mission.missionExplain);
            _rewardText.SetText($"기본보상: {_mission.missionDefaultReward}");
            _icon.sprite = _mission.icon;

            for(int i = 0; i < _difficultyObjects.Count; i++)
                _difficultyObjects[i].SetActive(i < _mission.missionDifficulty);

            _selectPanel = GetComponentInParent<MissionSelectPanel>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isSelected) return;

            _selectPanel.SelectPanel(this);

            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = childRect.DOAnchorPosY(0, 0.2f);
            _isSelected = true;

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isSelected) return;
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = childRect.DOAnchorPosY(0, 0.2f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isSelected) return;
            if (_tween != null && _tween.active)
                _tween.Kill();

            _tween = childRect.DOAnchorPosY(30, 0.2f);
        }
    }
}
