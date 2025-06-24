using Agents.Players.WeaponSystem;
using Chipset;
using Core.Attribute;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "SO/Shop/Goods")]
    public class ShopGoodsSO : ScriptableObject
    {
        public GoodsType goodsType;
        [Condition(nameof(goodsType), GoodsType.Chipset)] public ChipsetSO chipsetSO;
        [Condition(nameof(goodsType), GoodsType.Weapon)] public PlayerWeaponSO weaponSO;
        [Condition(nameof(goodsType), GoodsType.Heal)] public float healAmount;

        public int cost;

        public GoodsPreviewObject iconPrefab;
    }

    public enum GoodsType
    {
        Chipset,
        Weapon,
        Heal
    }
}
