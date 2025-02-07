
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Basement
{
    public class BasementBuildUI : MonoBehaviour, IUIPanel
    {
        [SerializeField] private BasementSO _basementInfo;
        [SerializeField] private GameObject _buildUIPrefab;
        [SerializeField] private Toggle toggle;

        public List<RoomBuildPositionStruct> roomPositions;

        private void Awake()
        {
            toggle.onValueChange.AddListener(ToggleValueChange);
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
                    {
                        if (roomPositions[i].roomPositions[j].childCount > 0)
                            Destroy(roomPositions[i].roomPositions[j].GetChild(0).gameObject);

                        continue;
                    }

                    if (roomPositions[i].roomPositions[j].childCount > 0)
                    {
                        roomPositions[i].roomPositions[j].GetChild(0).gameObject.SetActive(true);
                        continue;
                    }

                    Instantiate(_buildUIPrefab, roomPositions[i].roomPositions[j]);
                }
            }
        }

        public void Close()
        {
            for (int i = 0; i < roomPositions.Count; i++)
            {
                for (int j = 0; j < roomPositions[i].roomPositions.Count; j++)
                {
                    if (roomPositions[i].roomPositions[j].childCount > 0)
                    {
                        roomPositions[i].roomPositions[j].GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    [Serializable]
    public struct RoomBuildPositionStruct
    {
        public List<Transform> roomPositions;
    }
}
