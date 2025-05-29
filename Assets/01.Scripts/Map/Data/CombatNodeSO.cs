using Map;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu(menuName = "SO/Map/Node/CombatNode")]
    public class CombatNodeSO : MapNodeSO
    {
        //나오는 적 정보 [ 웨이브 ]

        public CombatNodeSO()
        {
            nodeType = NodeType.Combat;
        }

        public override MapNodeSO Instantiate()
        {
            CombatNodeSO combatNode = ScriptableObject.Instantiate(this);
            combatNode.nodeId = nodeId;
            combatNode.nodeType = nodeType;
            combatNode.displayName = displayName;
            combatNode.explain = explain;
            combatNode.icon = icon;
            combatNode.color = color;
            combatNode.nextNodes = nextNodes;
            return combatNode;
        }
    }
}
