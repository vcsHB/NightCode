using UnityEngine;

namespace Map
{
    [CreateAssetMenu(menuName = "SO/Map/Node/ShopNode")]
    public class ShopNodeSO : MapNodeSO
    {
        //상점 정보

        public ShopNodeSO()
        {
            nodeType = NodeType.Shop;
        }

        public override MapNodeSO Instantiate()
        {
            ShopNodeSO shopNodeSO = ScriptableObject.Instantiate(this);
            shopNodeSO.nodeId = nodeId;
            shopNodeSO.nodeType = nodeType;
            shopNodeSO.displayName = displayName;
            shopNodeSO.explain = explain;
            shopNodeSO.icon = icon;
            shopNodeSO.color = color;
            shopNodeSO.nextNodes = nextNodes;
            return shopNodeSO;
        }
    }
}
