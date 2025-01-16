using UnityEngine;
namespace Agents.Players
{
    [System.Serializable]
    public class AttackData
    {   
        public float movePower;
        public float moveduration;
    }
    public class PlayerAttackController : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private AttackData[] _attackDatas;

        

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void Initialize(Agent agent)
        {
        }


        public AttackData GetAttackData(int index)
        {
            if(index >= _attackDatas.Length)
            {
                Debug.LogWarning("AttackData Not Found : Index Range Over");
                return null;
            }
            return _attackDatas[index];
        }
    }
}