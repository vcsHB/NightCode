using Core.DataControl;
using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class Merchant : MonoBehaviour
    {
        public List<ShopGoodsSO> exsistGoods;
        public Sprite happyFace;

        public int standCount = 3;
        public List<DisplayStand> displayStands;
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
                if (goods[i].goodsType == GoodsType.Weapon && DataLoader.Instance.IsWeaponExist(goods[i].id)) continue;
                displayStands[i].SetGoods(goods[i]);
                displayStands[i].onBuyGoods += BuyHandler;
            }
        }

        private void BuyHandler()
        {
            face.sprite = happyFace;
        }
    }
}
