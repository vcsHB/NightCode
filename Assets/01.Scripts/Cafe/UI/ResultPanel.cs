using DG.Tweening;
using Office;
using System.Collections;
using TMPro;
using UI;
using UnityEngine;

namespace Cafe
{
    public class ResultPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private MissionSelectButton _missionSelect;
        [SerializeField] private TextMeshProUGUI _ratingText;
        [SerializeField] private NumberPassing customerNumber;
        [SerializeField] private NumberPassing rewardNumber;

        private float _duration = 0.5f;
        private Tween _openCloseTween;

        public RectTransform RectTrm => transform as RectTransform;

        public void Init(MissionSO mission, int customer, int rating)
        {
            _missionSelect.Init(mission);

            int reward = mission.missionDefaultReward;

            switch (rating)
            {
                case 1: _ratingText.SetText("D"); break;
                case 2:
                    _ratingText.SetText("C");
                    reward = Mathf.CeilToInt(reward * 1.1f);
                    break;
                case 3: 
                    _ratingText.SetText("B");
                    reward = Mathf.CeilToInt(reward * 1.2f);
                    break;
                case 4: 
                    _ratingText.SetText("A");
                    reward = Mathf.CeilToInt(reward * 1.4f);
                    break;
                case 5:
                    _ratingText.SetText("S");
                    reward = Mathf.CeilToInt(reward * 1.5f);
                    break;
            }

            StartCoroutine(InitRoutine(customer, reward));
        }

        private IEnumerator InitRoutine(int customer, int reward)
        {
            yield return new WaitForSeconds(_duration);
            customerNumber.SetText(customer);
            yield return new WaitForSeconds(customerNumber.Duration);
            rewardNumber.SetText(reward);
        }

        public void Close()
        {
            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = RectTrm.DOAnchorPosY(1080, _duration);
        }

        public void Open()
        {
            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = RectTrm.DOAnchorPosY(0, _duration);
        }
    }
}
