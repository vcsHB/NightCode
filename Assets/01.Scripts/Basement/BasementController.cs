using Basement.CameraController;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Basement
{
    public class BasementController : MonoBehaviour
    {
        [SerializeField] private GameObject _buildModeObj;
        [SerializeField] private GameObject _basementModeObj;

        private BasementMode _currentMode = BasementMode.Basement;

        public void ChangeBuildMode(bool isBuildMode)
        {
            if (isBuildMode) ChangeBasementMode(BasementMode.Build);
            else ChangeBasementMode(BasementMode.Basement);
        }

        public void ChangeBasementMode(BasementMode mode)
        {
            _currentMode = mode;

            if (mode == BasementMode.Basement)
            {
                _buildModeObj.SetActive(false);
            }
            else
            {
                _buildModeObj.SetActive(true);
            }
        }

        public BasementMode GetCurrentMode()
            => _currentMode;

    }

    public enum BasementMode
    {
        Basement,
        Build
    }
}