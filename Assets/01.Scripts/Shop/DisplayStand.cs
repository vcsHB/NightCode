using Agents.Players.WeaponSystem;
using Chipset;
using Combat.PlayerTagSystem;
using Core.DataControl;
using System.Text;
using TMPro;
using UnityEngine;

namespace Shop
{
    public class DisplayStand : MonoBehaviour
    {
        public ChipsetGruopSO chipsetGroup;
        public PlayerWeaponListSO weaponList;

        public TextMeshPro tmp;
        public GameObject textObject;
        public Transform iconTrm;

        public ShopGoodsSO shopGoods;

        private bool isGoodsExsist = true;
        private GameObject goodsSprite;

        public void SetGoods(ShopGoodsSO shopGoods)
        {
            this.shopGoods = shopGoods;

            StringBuilder text = new StringBuilder();
            text.Append($"{shopGoods.cost} <color=white>");

            switch(shopGoods.goodsType)
            {
                case GoodsType.Chipset:
                    ChipsetSO chispetSO = chipsetGroup.GetChipset((ushort)shopGoods.id);
                    text.Append($"{chispetSO.chipsetName}");
                    break;
                case GoodsType.Weapon:
                    PlayerWeaponSO weaponSO = weaponList.GetWeapon(shopGoods.id);
                    text.Append($"{weaponSO.weaponName}");
                    break;
                case GoodsType.Heal:
                    text.Append($"¼ö¸®Å¶ ( + {shopGoods.id} )");
                    break;
            }

            goodsSprite = Instantiate(shopGoods.iconPrefab, iconTrm);
            tmp.SetText(text.ToString());
        }

        public void BuyGoods()
        {
            if (isGoodsExsist == false) return;
            if (DataLoader.Instance.Credit < shopGoods.cost) return;

            switch (shopGoods.goodsType)
            {
                case GoodsType.Chipset:
                    DataLoader.Instance.AddChipset((ushort)shopGoods.id);
                    break;
                case GoodsType.Weapon:
                    DataLoader.Instance.AddWeapon(shopGoods.id);
                    break;
                case GoodsType.Heal:
                    PlayerManager.Instance.playerList.ForEach(player => player.HealthCompo.Restore(shopGoods.id));
                    break;
            }

            isGoodsExsist = false;
            textObject.gameObject.SetActive(false);
            CreditCollector.Instance.UseCredit(shopGoods.cost);
            Destroy(goodsSprite);
        }

        public void OnEnter()
        {
            if (isGoodsExsist == false || shopGoods == null) return;
            textObject.gameObject.SetActive(true);
        }

        public void OnExit()
        {
            if (isGoodsExsist == false || shopGoods == null) return;
            textObject.gameObject.SetActive(false);
        }
    }
}
