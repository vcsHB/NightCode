using Office;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace Core.StageController
{
    [CreateAssetMenu(fileName = "StageSetSO", menuName = "SO/Office/StageSetSO")]
    public class StageSetSO : ScriptableObject
    {
        public List<StageSO> stageList = new ();

#if UNITY_EDITOR
        public StageSO CreateMission(Type type)
        {
            StageSO stage = ScriptableObject.CreateInstance(type) as StageSO;

            stage.id = (ushort)stageList.Count;
            stage.name = $"{type.Name}-{stage.id}";
            stage.guid = GUID.Generate().ToString();
            stage.nextMissions = new List<StageSO>();
            stageList.Add(stage);

            AssetDatabase.AddObjectToAsset(stage, this);
            AssetDatabase.SaveAssets();
            return stage;
        }

        public void DeleteScript(StageSO mission)
        {
            stageList.Remove(mission);
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
