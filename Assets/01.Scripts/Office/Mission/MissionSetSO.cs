using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace Office
{
    [CreateAssetMenu(fileName = "MissionSetSO", menuName = "SO/Office/MissionSetSO")]
    public class MissionSetSO : ScriptableObject
    {
        public List<MissionSO> missionList = new ();

#if UNITY_EDITOR
        public MissionSO CreateMission()
        {
            MissionSO mission = ScriptableObject.CreateInstance<MissionSO>();

            mission.id = (ushort)missionList.Count;
            mission.name = $"Mission-{mission.id}";
            mission.guid = GUID.Generate().ToString();
            mission.nextMissions = new List<MissionSO>();
            missionList.Add(mission);

            AssetDatabase.AddObjectToAsset(mission, this);
            AssetDatabase.SaveAssets();
            return mission;
        }

        public void DeleteScript(MissionSO mission)
        {
            missionList.Remove(mission);
            AssetDatabase.RemoveObjectFromAsset(mission);
            AssetDatabase.SaveAssets();
        }

        public List<MissionSO> GetConnectedMissions(MissionSO mission) 
            => mission.nextMissions;
        public MissionSO GetPrevNode(MissionSO mission) => mission.prevMission;

        public void RemoveNextNode(MissionSO parent, MissionSO child)
        {
            parent.nextMissions
                .Where((mission) => mission.id == child.id)
                .ToList()
                .ForEach((node) =>
                {
                    parent.nextMissions.Remove(node);
                    node.prevMission = null;
                });
        }
        public void AddNextNode(MissionSO parent, MissionSO child)
        {
            bool isExsist = parent.nextMissions.Where((node) => node.id == child.id).Count() > 0;
            if (!isExsist)
            {
                parent.nextMissions.Add(child);
                child.prevMission = parent;
            }
        }
#endif
    }
}
