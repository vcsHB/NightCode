using DG.Tweening;
using TMPro;
using UnityEngine;
namespace SpeedRun
{

    public class CountDownTextObject : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textCompo;
        [SerializeField] private float _enableDuration = 0.4f;


        public void SetEnable(bool value)
        {
            transform.DOScaleX(value ? 1f : 0f, _enableDuration);
            _textCompo.text = string.Empty;
            _textCompo.enabled = value;
        }
        public void SetCountDownText(string content)
        {
            _textCompo.text = content;
        }
    }
}