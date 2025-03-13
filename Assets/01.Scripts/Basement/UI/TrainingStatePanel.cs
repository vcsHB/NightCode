
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Basement.Training
{
    public class TrainingStatePanel : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameTxt;
        [SerializeField] private TextMeshProUGUI _timeTxt;
        [SerializeField] private TextMeshProUGUI _roomPosition;
        [SerializeField] private List<Sprite> _iconList;

        public void SetInfo(CharacterEnum character, int remainTime, string trainingName)
        {
            //이건 나중에 바꾸도록
            _icon.sprite = _iconList[(int)character];
            _nameTxt.SetText(character.ToString());
            _roomPosition.SetText($"{trainingName}");

            if (remainTime >= 60)
                _timeTxt.SetText($"소요시간: {remainTime / 60}h {string.Format("{0,2:D2}", (remainTime % 60))}m");
            else
                _timeTxt.SetText($"소요시간: {string.Format("{0,2:D2}", remainTime % 60)}m");

        }
    }
}
