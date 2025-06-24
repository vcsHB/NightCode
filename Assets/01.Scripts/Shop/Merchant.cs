using Core.DataControl;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shop
{
    public class Merchant : MonoBehaviour
    {
        [Header("Goods Setting")]
        public List<ShopGoodsSO> exsistGoods;
        [SerializeField] private ShopGoodsSO _healItemSO;

        [Header("Stand Setting")]
        [SerializeField] private DisplayStand _healItemStand;
        public List<DisplayStand> displayStands;

        [Header("Seller Setting")]
        public Sprite happyFace;
        public SpriteRenderer face;

        public void Awake()
        {
            Init();
        }

        private void Init()
        {
            List<ShopGoodsSO> goods = RandomUtility.GetRandomsInListNotDuplicated(exsistGoods, displayStands.Count);

            for (int i = 0; i < displayStands.Count; i++)
            {
                if (goods[i].goodsType == GoodsType.Weapon)
                    if (DataLoader.Instance.IsWeaponExist(goods[i].weaponSO.id))
                    {
                        displayStands[i].SetEmptySlot();
                        continue;
                    }

                displayStands[i].SetGoods(goods[i]);
                displayStands[i].onBuyGoods += BuyHandler;
            }
            _healItemStand.SetGoods(_healItemSO);
            _healItemStand.onBuyGoods += BuyHandler;
        }

        private void BuyHandler()
        {
            face.sprite = happyFace;
        }
    }
}
