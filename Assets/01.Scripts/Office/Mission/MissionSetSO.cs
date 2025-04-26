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
        public List<StageSO> missionList = new ();

#if UNITY_EDITOR
        public StageSO CreateMission(Type type)
        {
            StageSO stage = ScriptableObject.CreateInstance(type) as StageSO;

            stage.id = (ushort)missionList.Count;
            stage.name = $"{type.Name}-{stage.id}";
            stage.guid = GUID.Generate().ToString();
            stage.nextMissions = new List<StageSO>();
            missionList.Add(stage);

            AssetDatabase.AddObjectToAsset(stage, this);
            AssetDatabase.SaveAssets();
            return stage;
        }

        public void DeleteScript(StageSO mission)
        {
            missionList.Remove(mission);
            AssetDatabase.RemoveObjectFromAsset(mission);
            AssetDatabase.SaveAssets();
        }

        public List<StageSO> GetConnectedMissions(StageSO mission) 
            => mission.nextMissions;
        public StageSO GetPrevNode(StageSO mission) => mission.prevMission;

        public void RemoveNextNode(StageSO parent, StageSO child)
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
        public void AddNextNode(StageSO parent, StageSO child)
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
