using UnityEngine;

namespace Map
{
    [CreateAssetMenu(menuName = "SO/Map/Node/EncounterNode")]
    public class EncounterNodeSO : MapNodeSO
    {
        //우현한 만남에 대한 정보

        public EncounterNodeSO()
        {
            nodeType = NodeType.Encounter;
        }


        public override MapNodeSO Instantiate()
        {
            EncounterNodeSO encounterNodeSO = ScriptableObject.Instantiate(this);
            encounterNodeSO.nodeId = nodeId;
            encounterNodeSO.nodeType = nodeType;
            encounterNodeSO.displayName = displayName;
            encounterNodeSO.explain = explain;
            encounterNodeSO.icon = icon;
            encounterNodeSO.color = color;
            encounterNodeSO.nextNodes = nextNodes;
            return encounterNodeSO;
        }
    }
}
