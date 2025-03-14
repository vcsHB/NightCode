using UnityEngine;
namespace ObjectManage.GimmickObjects
{
    [System.Serializable]
    public struct LogicData
    {
        public string logicId;
    }

    public interface ISovleable 
    {
        public void Solve();
        
    }
}