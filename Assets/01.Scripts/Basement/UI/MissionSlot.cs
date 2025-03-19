using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Basement.Mission
{
    public class MissionSlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _missionName;
        [SerializeField] private List<GameObject> _difficultyIconList;
        private MissionSO _mission;
        private MissionSelectPanel _selectPanel;

        public void SetMission(MissionSelectPanel selectPanel, MissionSO mission)
        {
            _selectPanel = selectPanel;
            _mission = mission;

            _missionName.SetText(mission.missionName);

            for(int i = 0; i < _difficultyIconList.Count; i++)
                _difficultyIconList[i].SetActive(i < mission.missionDifficulty);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _selectPanel.SelectPanel(_mission);
        }
    }
}
