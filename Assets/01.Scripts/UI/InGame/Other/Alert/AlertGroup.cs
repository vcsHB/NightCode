using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI.InGame.SystemUI.AlertSystem
{
    public enum AlertType
    {
        Normal,
        Warning,
        Danger
    }
    public class AlertGroup : MonoBehaviour
    {
        [SerializeField] private AlertBox _boxPrefab;
        [SerializeField] private RectTransform _generatePositionTrm;
        [SerializeField] private RectTransform _contentTrm;
        [Header("AlertAudio Setting")]
        [SerializeField] private AudioClip _normalAlertSound;
        [SerializeField] private AudioClip _warningAlertSound;
        [SerializeField] private AudioClip _dangerAlertSound;

        private Queue<AlertBox> _boxPool = new();
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }


        public void ShowAlert(string content, AlertType type = AlertType.Normal)
        {
            AlertBox alertBox = null;
            if (_boxPool.Count <= 0)
                alertBox = Instantiate(_boxPrefab, _contentTrm);
            else
                alertBox = _boxPool.Dequeue();

            alertBox.SetPos(_generatePositionTrm.anchoredPosition);
            alertBox.SetAlert(content);
        }

        private void PlayAlertSound(AlertType type)
        {
            AudioClip sound = _normalAlertSound;
            switch (type)
            {
                case AlertType.Warning:
                    break;
                case AlertType.Danger:
                    break;
            }
        }

    }
}