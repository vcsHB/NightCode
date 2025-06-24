using Agents.Players.WeaponSystem;
using Chipset;
using Combat.PlayerTagSystem;
using Core.DataControl;
using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Shop
{
    public class DisplayStand : MonoBehaviour
    {
        public UnityEvent OnPurchaseEvent;

        public event Action onBuyGoods;
        public ChipsetGroupSO chipsetGroup;
        public PlayerWeaponListSO weaponList;
        [SerializeField] private DescriptionPanel _descriptionPanel;

        public TextMeshPro tmp;
        public GameObject textObject;
        public Transform iconTrm;

        public ShopGoodsSO shopGoods;

        private bool isGoodsExist;
        private GoodsPreviewObject _goodsPreview;
        [SerializeField] GoodsPreviewObject _defaultGoodsPreview;
        public void SetEmptySlot()
        {
            _defaultGoodsPreview.gameObject.SetActive(true);
        }
        public void SetGoods(ShopGoodsSO shopGoods)
        {
            this.shopGoods = shopGoods;
            isGoodsExist = true;
            StringBuilder text = new StringBuilder();
            text.Append($"{shopGoods.cost} <color=white>");
            switch (shopGoods.goodsType)
            {
                case GoodsType.Chipset:
                    ChipsetSO chipsetSO = chipsetGroup.GetChipset((ushort)shopGoods.chipsetSO.id);
                    _descriptionPanel.SetContent(chipsetSO.chipsetName, chipsetSO.chipsetDescription);
                    text.Append($"{chipsetSO.chipsetName}");
                    break;
                case GoodsType.Weapon:
                    PlayerWeaponSO weaponSO = weaponList.GetWeapon(shopGoods.weaponSO.id);
                    _descriptionPanel.SetContent(weaponSO.weaponName, weaponSO.weaponDescription);
                    text.Append($"{weaponSO.weaponName}");
                    break;
                case GoodsType.Heal:
                    _descriptionPanel.SetContent("F.I.X KIT", $"체력을 {shopGoods.healAmount} 회복합니다");
                    text.Append($"F.I.X KIT ( + {shopGoods.healAmount} )");
                    break;
            }
            if (_defaultGoodsPreview != null)
                _defaultGoodsPreview.gameObject.SetActive(false);
            _goodsPreview = Instantiate(shopGoods.iconPrefab, iconTrm);

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
            OnPurchaseEvent?.Invoke();
            isGoodsExist = false;
            textObject.gameObject.SetActive(false);
            CreditCollector.Instance.UseCredit(shopGoods.cost);
            _goodsPreview.SetSoldOut();
            
        }

        public void OnEnter()
        {
            if (isGoodsExist == false || shopGoods == null) return;
            textObject.gameObject.SetActive(true);
            _descriptionPanel.Open();
        }

        public void OnExit()
        {
            if (isGoodsExist == false || shopGoods == null) return;
            textObject.gameObject.SetActive(false);
            _descriptionPanel.Close();
        }
    }
}
