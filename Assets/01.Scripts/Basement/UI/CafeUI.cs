using Basement.Training;
using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class CafeUI : BasementRoomCharacterPlaceUI
    {
        public TextMeshProUGUI profitText;
        public Button openStoreBtn;
        public TextMeshProUGUI openStoreText;
        public Button cancelBtn;
        [Space]
        public GameObject resultParent;
        public TextMeshProUGUI workTimeText;
        public TextMeshProUGUI totalProfiText;

        private Cafe _cafe;
        private Tween _tween;

        public void Init(Cafe cafe)
        {
            _cafe = cafe;
        }

        public void Open()
        {
            characterSelectDropDown.ClearOptions();
            List<TMP_Dropdown.OptionData> options = new();
            resultParent.SetActive(false);

            if (_cafe.isCafeOpen)
            {
                openStoreText.SetText("가게 닫기");
                openStoreBtn.onClick.AddListener(CloseStore);

                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
                optionData.text = _cafe.PositionedCharacter.ToString();
                options.Add(optionData);
            }
            else
            {
                openStoreText.SetText("가게 열기");
                openStoreBtn.onClick.AddListener(OpenStore);

                foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
                {
                    TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
                    optionData.text = character.ToString();
                    options.Add(optionData);
                }
            }
            characterSelectDropDown.AddOptions(options);
            OnSelectCharacter(0);

            if (_tween != null && _tween.active) _tween.Kill();
            _tween = transform.DOScale(1, 0.2f);
        }

        public void Close()
        {
            cancelBtn.gameObject.SetActive(true);
            openStoreBtn.onClick.RemoveListener(OpenStore);
            openStoreBtn.onClick.RemoveListener(CloseStore);
            openStoreBtn.onClick.RemoveListener(Close);

            if (_tween != null && _tween.active) _tween.Kill();
            _tween = transform.DOScale(0, 0.2f);
        }

        protected override void OnSelectCharacter(int value)
        {
            base.OnSelectCharacter(value);
            icon.gameObject.SetActive(true);
            profitText.SetText($"수익: {_cafe.profitRange.x} ~ {_cafe.profitRange.y}/1h");
            _cafe.PositionedCharacter = (CharacterEnum)value;
        }

        public void OpenStore()
        {
            UIManager.Instance.msgText.PopMSGText(_cafe.PositionedCharacter, "가게 열림");

            _cafe.isCafeOpen = true;
            _cafe.cafeOpenTime = WorkManager.Instance.CurrentTime;
            Close();
        }

        private void CloseStore()
        {
            //결과창 띄우기
            resultParent.SetActive(true);
            BasementTime startTime = _cafe.cafeOpenTime;
            BasementTime endTime = WorkManager.Instance.CurrentTime;

            int passedHour = endTime.hour - startTime.hour;
            int passedMinute = endTime.minute - startTime.minute;

            string timeText = $"{startTime.ToTimeText()}:{endTime.ToTimeText()}";
            string passedTimeText = (passedHour > 0 || passedMinute > 0) ? $"({(passedHour > 0 ? $"{passedHour}시간" : "")}{(passedMinute > 0 ? $"{passedMinute}분" : "")})" : "";

            workTimeText.SetText($"{timeText} {passedTimeText}");
            totalProfiText.SetText($"총수입: {_cafe.totalProfit}");

            _cafe.isCafeOpen = false;
            cancelBtn.gameObject.SetActive(false);
            openStoreText.SetText("닫기");

            openStoreBtn.onClick.RemoveListener(CloseStore);
            openStoreBtn.onClick.AddListener(Close);
        }
    }
}
