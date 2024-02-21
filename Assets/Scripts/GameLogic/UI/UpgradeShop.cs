using System;
using GameLogic.BoatLogic;
using GameLogic.Configs;
using GameLogic.Entities;
using Infrastructure.Services;
using UnityEngine;

namespace GameLogic.UI
{
    public class UpgradeShop
    {
    
        private readonly GameControlService _gameControlService;
        private readonly PlayerStatsChangerService _playerStatsChangerService;

        public UpgradeShop( GameControlService gameControlService, 
            PlayerStatsChangerService playerStatsChangerService)
        {
            _playerStatsChangerService = playerStatsChangerService;
            _gameControlService = gameControlService;
        }
        
        public void BuyUpgrade(UpgradeItem upgradeItem, Action<UpgradeItem> onUpgradeBought = null)
        {
            int upgradeLevel = _playerStatsChangerService.GetUpgradeLevel(upgradeItem.upgradeType);
            if (_playerStatsChangerService.GetCoins() >= upgradeItem.cost[upgradeLevel])
            {
                _playerStatsChangerService.SubtractCoins(upgradeItem.cost[upgradeLevel]);
                _gameControlService.UpgradeLevelUp(upgradeItem);
                onUpgradeBought?.Invoke(upgradeItem);
            }
            else
            {
                Debug.Log("Not enough currency or upgrade not found.");
            }
        }
        
    }
}