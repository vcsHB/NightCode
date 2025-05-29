using UnityEngine;

namespace Map
{
    [CreateAssetMenu(menuName = "SO/Map/Node/BossNode")]
    public class BossNodeSO : MapNodeSO
    {
        public BossNodeSO()
        {
            nodeType = NodeType.Boss;
        }

        public override MapNodeSO Instantiate()
        {
            BossNodeSO bossNode = ScriptableObject.Instantiate(this);
            bossNode.nodeId = nodeId;
            bossNode.nodeType = nodeType;
            bossNode.displayName = displayName;
            bossNode.explain = explain;
            bossNode.icon = icon;
            bossNode.color = color;
            bossNode.nextNodes = nextNodes;
            return bossNode;
        }
    }
}
