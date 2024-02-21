using System;
using GameLogic.Entities;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.UI
{
    public class ShopUIItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI upgradeName;
        [SerializeField] private TextMeshProUGUI upgradeCost;
        [SerializeField] private TextMeshProUGUI upgradeValue;
        [SerializeField] private Image upgradeImage;
        [SerializeField] private Button _selectButton;
        public event Action<UpgradeItem> OnItemSelected;

        private UpgradeItem _item;
        private PlayerStatsChangerService _playerStatsChangerService;
        
        private void Start() =>
            _selectButton.onClick.AddListener(SelectItem);

        public void SetItem(UpgradeItem item, ShopUI shopUI, PlayerStatsChangerService playerStatsChangerService)
        {
            
            _playerStatsChangerService = playerStatsChangerService;
            int upgradeLevel = _playerStatsChangerService.GetUpgradeLevel(item.upgradeType);
            CheckUpgradeLevel(item, shopUI, upgradeLevel);
            
            _item = item;
            upgradeName.text = item.itemName;
            upgradeCost.text = item.cost[upgradeLevel].ToString();
            SetUpgradeValue(item, upgradeLevel);
            SetImage(item, upgradeLevel);
        }

        private void CheckUpgradeLevel(UpgradeItem item, ShopUI shopUI, int upgradeLevel)
        {
            if (upgradeLevel >= item.value.Length - 1)
            {
                shopUI.RemoveItem(this);
                Destroy(gameObject);
            }
        }

        private void SetImage(UpgradeItem item, int upgradeLevel)
        {
            upgradeImage.sprite = item.image.Length > 1 ? item.image[upgradeLevel] : item.image[0];
        }

        private void SetUpgradeValue(UpgradeItem item, int upgradeLevel)
        {
            switch (item.upgradeType)
            {
                case UpgradeType.ModelType:
                case UpgradeType.WeaponType:
                    upgradeValue.text = String.Empty;
                    break;
                case UpgradeType.FireRate:
                case UpgradeType.Damage:
                    upgradeValue.text = "+" + item.value[upgradeLevel] + "%";
                    break;
                default:
                    upgradeValue.text = "+" + item.value[upgradeLevel];
                    break;
            }
        }


        private void SelectItem() =>
            OnItemSelected?.Invoke(_item);
    }
}