using System;
using TMPro;
using UnityEngine;
namespace UI.SpeedRun
{

    public class SpeedRunChallengePanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _nameInputField;

        private void Awake()
        {
            _nameInputField.onValueChanged.AddListener(HandleInputChanged);
        }

        private void HandleInputChanged(string arg0)
        {

        }
    }
}