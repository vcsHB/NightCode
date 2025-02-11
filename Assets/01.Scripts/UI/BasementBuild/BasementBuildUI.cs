
using Basement.CameraController;
using Basement.Player;
using CameraControllers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Basement
{
    public class BasementBuildUI : MonoBehaviour, IUIPanel
    {
        [SerializeField] private BasementSO _basementInfo;
        [SerializeField] private BuildingSelectPanel _buildingSelectPanel;
        [SerializeField] private BasementPlayer _player;
        [SerializeField] private Toggle toggle;

        public List<RoomBuildPositionStruct> basementBuildUI;

        private void Awake()
        {
            toggle.onValueChange.AddListener(ToggleValueChange);

            for (int i = 0; i < basementBuildUI.Count; i++)
            {
                for (int j = 0; j < basementBuildUI[i].roomBuildUI.Count; j++)
                {
                    basementBuildUI[i].roomBuildUI[j].Init(i, j, _buildingSelectPanel);
                }
            }
        }

        private void ToggleValueChange(bool isOpen)
        {
            if (isOpen)
            {
                Open(Vector2.zero);
            }
            else
            {
                Close();
            }
        }

        public void Open(Vector2 position)
        {
            for (int i = 0; i < _basementInfo.expendedFloor; i++)
            {
                FloorInfo floor = _basementInfo.floorInfos[i];
                for (int j = 0; j < floor.rooms.Count; j++)
                {
                    if (floor.rooms[j].roomType != BasementRoomType.Empty)
                        continue;

                    BuildUI buildUI = basementBuildUI[i].roomBuildUI[j];
                    buildUI.gameObject.SetActive(true);
                    buildUI.Init(i, j, _buildingSelectPanel);
                }
            }

            BasementCameraManager.Instance.ChangeCameraMode(CameraMode.Build);
        }

        public void Close()
        {
            for (int i = 0; i < basementBuildUI.Count; i++)
            {
                for (int j = 0; j < basementBuildUI[i].roomBuildUI.Count; j++)
                {
                    basementBuildUI[i].roomBuildUI[j].gameObject.SetActive(false);
                }
            }

            BasementCameraManager.Instance.ChangeCameraMode(CameraMode.Basement);
        }

        public void ExpendFloor()
        {
            _basementInfo.expendedFloor++;
        }
    }

    [Serializable]
    public struct RoomBuildPositionStruct
    {
        public List<BuildUI> roomBuildUI;
    }
}
