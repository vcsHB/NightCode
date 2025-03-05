using Basement.CameraController;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Basement
{
    public class BasementController : MonoBehaviour
    {
        [SerializeField] private GameObject _buildModeObj;
        [SerializeField] private GameObject _basementModeObj;
        private List<BasementBuildUI> _buildUISet;

        private BasementMode _currentMode = BasementMode.Basement;

        private void Awake()
        {
            _buildUISet = _buildModeObj.GetComponentsInChildren<BasementBuildUI>().ToList();
        }

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
                _buildUISet.ForEach(buildUI => buildUI.Close());
            }
            else
            {
                _buildUISet.ForEach(buildUI => buildUI.Open());
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