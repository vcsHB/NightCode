using Basement.CameraController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basement
{
    public class BasementBuildUI : MonoBehaviour
    {
        public List<BuildUISet> buildUIs;
        public List<GameObject> floorExpendUI;
        public BuildingSelectPanel buildingSelectPanel;
        private BasementSO basementInfo;

        private void Start()
        {
            basementInfo = BasementManager.Instance.basementInfo;

            for (int i = 0; i < basementInfo.maxFloor; i++)
            {
                for (int j = 0; j < basementInfo.floorInfos[i].rooms.Count; j++)
                {
                    BuildUI buildUI = buildUIs[i].buildUI[j];
                    buildUI.Init(i, j, buildingSelectPanel);
                }
            }
        }

        public void ExpendFloor()
        {
            if (basementInfo.expendedFloor >= basementInfo.maxFloor) return;

            int currentFloor = basementInfo.expendedFloor++;

            floorExpendUI[currentFloor].SetActive(false);
            floorExpendUI[currentFloor + 1].SetActive(true);
            EnableBuildUI();

            BasementCameraManager.Instance.ChangeFollowToFloor(currentFloor + 1, 0.3f,
                () =>
                {
                    SetDelay(0.5f,
                        () => BasementCameraManager.Instance.ChangeFollowToFloor(currentFloor, 0.3f, null));
                });
        }

        public void TurnOnBuildMode(bool isBuildMode)
        {
            if (isBuildMode) EnableBuildUI();
            else DisableBuildUI();

            floorExpendUI[basementInfo.expendedFloor].SetActive(isBuildMode);
            BasementCameraManager.Instance.ChangeCameraMode(isBuildMode ? CameraMode.Build : CameraMode.Basement);
        }

        public void BuildBuilding(BasementRoomType roomType, int floor, int roomNumber)
        {
            BuildUI buildUI = buildUIs[floor].buildUI[roomNumber];
            BasementManager.Instance.CreateRoom(roomType, floor, roomNumber);
        }

        public void EnableBuildUI()
        {
            basementInfo = BasementManager.Instance.basementInfo;

            for (int i = 0; i <= basementInfo.expendedFloor; i++)
            {
                for (int j = 0; j < basementInfo.floorInfos[i].rooms.Count; j++)
                {
                    if (basementInfo.floorInfos[i].rooms[j].roomType != BasementRoomType.Empty) continue;
                    buildUIs[i].buildUI[j].gameObject.SetActive(true);
                }
            }
        }

        public void DisableBuildUI()
        {
            basementInfo = BasementManager.Instance.basementInfo;

            for (int i = 0; i < basementInfo.maxFloor; i++)
            {
                for (int j = 0; j < basementInfo.floorInfos[i].rooms.Count; j++)
                {
                    buildUIs[i].buildUI[j].gameObject.SetActive(false);
                }
            }
        }

        private void SetDelay(float delay, Action action)
            => StartCoroutine(SetDelayRoutine(delay, action));

        private IEnumerator SetDelayRoutine(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }

    [Serializable]
    public struct BuildUISet
    {
        public List<BuildUI> buildUI;
    }
}
