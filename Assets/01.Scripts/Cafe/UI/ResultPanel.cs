using DG.Tweening;
using Office;
using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cafe
{
    public class ResultPanel : MonoBehaviour, IWindowPanel
    {
        //[SerializeField] private MissionSelectButton _missionSelect;
        [SerializeField] private TextMeshProUGUI _ratingText;
        [SerializeField] private NumberPassing _customerNumber;
        [SerializeField] private NumberPassing _rewardNumber;

        [Space]
        [SerializeField] private GameObject _missionType;
        [SerializeField] private GameObject _rating;
        [SerializeField] private GameObject _customerCount;
        [SerializeField] private GameObject _reward;
        [SerializeField] private GameObject _successObj;
        [SerializeField] private GameObject _returnButton;

        private float _duration = 0.5f;
        private float _delay = 0.5f;
        private Tween _openCloseTween;

        public RectTransform RectTrm => transform as RectTransform;

        private void Awake()
        {
            RectTrm.anchoredPosition = new Vector2(0, Screen.height);
        }

        public void Init(StageSO mission, int customer, int rating)
        {
            rating = Mathf.Clamp(rating, 1, 5);
            //_missionSelect.Init(mission);

            //int reward = mission.missionDefaultReward;

            //switch (rating)
            //{
            //    case 1: _ratingText.SetText("D"); break;
            //    case 2:
            //        _ratingText.SetText("C");
            //        reward = Mathf.CeilToInt(reward * 1.1f);
            //        break;
            //    case 3: 
            //        _ratingText.SetText("B");
            //        reward = Mathf.CeilToInt(reward * 1.2f);
            //        break;
            //    case 4: 
            //        _ratingText.SetText("A");
            //        reward = Mathf.CeilToInt(reward * 1.4f);
            //        break;
            //    case 5:
            //        _ratingText.SetText("S");
            //        reward = Mathf.CeilToInt(reward * 1.5f);
            //        break;
            //}

            //LayoutRebuilder.ForceRebuildLayoutImmediate(RectTrm);
            //StartCoroutine(InitRoutine(customer, reward));
        }

        private IEnumerator InitRoutine(int customer, int reward)
        {
            yield return new WaitForSeconds(_duration + _delay);
            _missionType.SetActive(true);
            yield return new WaitForSeconds(_delay);
            _rating.SetActive(true);
            yield return new WaitForSeconds(_delay);
            _customerCount.SetActive(true);
            _customerNumber.duration = _duration;
            _customerNumber.SetText(customer);
            yield return new WaitForSeconds(_duration + _delay);
            _reward.SetActive(true);
            _rewardNumber.duration = _duration;
            _rewardNumber.SetText(reward);
            yield return new WaitForSeconds(_duration + _delay);
            _successObj.SetActive(true);
            yield return new WaitForSeconds(_delay);
            _returnButton.SetActive(true);
        }

        public void ReturnToOffice()
        {
            SceneManager.LoadScene("Office_Scene");
        }

        public void Close()
        {
            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = RectTrm.DOAnchorPosY(Screen.height, _duration);
        }

        public void Open()
        {
            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = RectTrm.DOAnchorPosY(0, _duration)
                .OnComplete(() => _ratingText.gameObject.SetActive(true));
        }
    }
}
