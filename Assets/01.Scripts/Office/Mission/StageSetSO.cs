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
        public StageSO CreateStage(Type type)
        {
            StageSO stage = ScriptableObject.CreateInstance(type) as StageSO;
            if (stage == null) return null;

            stage.id = (ushort)stageList.Count;
            stage.name = $"{stage.id}.{stage.displayStageName}";
            stage.guid = GUID.Generate().ToString();
            stage.nextStage = null;
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

        public StageSO GetConnectedMissions(StageSO mission) 
            => mission.nextStage;
        public StageSO GetPrevNode(StageSO mission) => mission.prevStage;

        public void RemoveNextNode(StageSO parent, StageSO child)
        {
            if (parent.nextStage.id != child.id) return;
            parent.nextStage = null;
        }
        public void AddNextNode(StageSO parent, StageSO child)
        {
            parent.nextStage = child;
        }
#endif
    }
}
