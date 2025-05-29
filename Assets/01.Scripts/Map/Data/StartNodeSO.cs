using UnityEngine;

namespace Map
{
    [CreateAssetMenu(menuName = "SO/Map/Node/StartNode")]
    public class StartNodeSO : MapNodeSO
    {
        public StartNodeSO() 
        {
            nodeType = NodeType.Start;
        }

        public override MapNodeSO Instantiate()
        {
            StartNodeSO startNodeSO = ScriptableObject.Instantiate(this);
            startNodeSO.nodeId = nodeId;
            startNodeSO.nodeType = nodeType;
            startNodeSO.displayName = displayName;
            startNodeSO.explain = explain;
            startNodeSO.icon = icon;
            startNodeSO.color = color;
            startNodeSO.nextNodes = nextNodes;
            return startNodeSO;
        }
    }
}
