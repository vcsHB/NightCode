using UnityEngine;
namespace ObjectManage.GimmickObjects
{
    [System.Serializable]
    public struct LogicData
    {
        public int intValue;
        public float floatValue;
        public string stringValue;
    }

    public interface ISovleable 
    {
        public bool IsSolved{ get; set;}

        public void ResetGimmick();
        public void Solve(LogicData data);
        
    }
}