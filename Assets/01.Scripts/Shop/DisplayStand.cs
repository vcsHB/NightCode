using Agents.Players.WeaponSystem;
using Chipset;
using Combat.PlayerTagSystem;
using Core.DataControl;
using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace Shop
{
    public class DisplayStand : MonoBehaviour
    {
        public event Action onBuyGoods;
        public ChipsetGruopSO chipsetGroup;
        public PlayerWeaponListSO weaponList;

        public TextMeshPro tmp;
        public GameObject textObject;
        public Transform iconTrm;

        public ShopGoodsSO shopGoods;

        private bool isGoodsExist;
        private GameObject goodsSprite;

        public void SetGoods(ShopGoodsSO shopGoods)
        {
            this.shopGoods = shopGoods;
            isGoodsExist = true;
            StringBuilder text = new StringBuilder();
            text.Append($"{shopGoods.cost} <color=white>");

            switch (shopGoods.goodsType)
            {
                case GoodsType.Chipset:
                    ChipsetSO chispetSO = chipsetGroup.GetChipset((ushort)shopGoods.chipsetSO.id);
                    text.Append($"{chispetSO.chipsetName}");
                    break;
                case GoodsType.Weapon:
                    PlayerWeaponSO weaponSO = weaponList.GetWeapon(shopGoods.weaponSO.id);
                    text.Append($"{weaponSO.weaponName}");
                    break;
                case GoodsType.Heal:
                    text.Append($"F.I.X KIT ( + {shopGoods.healAmount} )");
                    break;
            }

            goodsSprite = Instantiate(shopGoods.iconPrefab, iconTrm);
            tmp.SetText(text.ToString());
        }

        public void BuyGoods()
        {
            if (isGoodsExist == false) return;
            if (DataLoader.Instance.Credit < shopGoods.cost) return;

            switch (shopGoods.goodsType)
            {
                case GoodsType.Chipset:
                    DataLoader.Instance.AddChipset((ushort)shopGoods.chipsetSO.id);
                    break;
                case GoodsType.Weapon:
                    DataLoader.Instance.AddWeapon(shopGoods.weaponSO.id);
                    break;
                case GoodsType.Heal:
                    PlayerManager.Instance.playerList.ForEach(player => player.HealthCompo.Restore(shopGoods.healAmount));
                    break;
            }

            onBuyGoods?.Invoke();
            isGoodsExist = false;
            textObject.gameObject.SetActive(false);
            CreditCollector.Instance.UseCredit(shopGoods.cost);
            Destroy(goodsSprite);
        }

        public void OnEnter()
        {
            if (isGoodsExist == false || shopGoods == null) return;
            textObject.gameObject.SetActive(true);
        }

        public void OnExit()
        {
            if (isGoodsExist == false || shopGoods == null) return;
            textObject.gameObject.SetActive(false);
        }
    }
}
