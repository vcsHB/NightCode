using SpeedRun;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI.SpeedRun
{
    public class SpeedRunChallengePanel : MonoBehaviour
    {
        public UnityEvent OnAllowedStartEvent;

        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private GameObject[] _alertTexts; // 0: Allow / 1+ : Deny
        [SerializeField] private SpeedRunManager _manager;
        [SerializeField] private SpeedRunDataController _dataController;
        [SerializeField] private int _minNameLength = 3;
        [SerializeField] private int _maxNameLength = 15;

        private bool _allowed;

        private void Awake()
        {
            _nameInputField.onValueChanged.AddListener(HandleInputChanged);
            _manager.OnSpeedRunResetEvent.AddListener(ResetChallengerData);
        }

        public void ResetChallengerData()
        {
            _nameInputField.text = string.Empty;
            _allowed = false;
            DisableAllAlert();
        }

        private void HandleInputChanged(string name)
        {
            DisableAllAlert();
            _allowed = false;

            int length = name.Length;

            if (length < _minNameLength)
            {
                SetEnableAlert(1);
                return;
            }
            if (length > _maxNameLength)
            {
                SetEnableAlert(2);
                return;
            }
            if (_dataController.RecordData.IsDuplicatedRecord(name))
            {
                SetEnableAlert(3);
                return;
            }

            SetEnableAlert(0);
            _allowed = true;
        }

        private void DisableAllAlert()
        {
            foreach (var alert in _alertTexts)
                alert.SetActive(false);
        }

        private void SetEnableAlert(int index)
        {
            _alertTexts[index].SetActive(true);
        }

        public void HandleStartSpeedRun()
        {
            if (!_allowed) return;

            _dataController.InitializeChallenger(_nameInputField.text);
            OnAllowedStartEvent?.Invoke();
        }
    }
}
