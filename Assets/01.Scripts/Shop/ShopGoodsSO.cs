using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "SO/Shop/Goods")]
    public class ShopGoodsSO : ScriptableObject
    {
        public GoodsType goodsType;
        public int id;
        public int cost;

        public GameObject iconPrefab;
    }

    public enum GoodsType
    {
        Chipset,
        Weapon,
        Heal
    }
}
