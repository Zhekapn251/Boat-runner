using System;
using GameLogic.Entities;
using GameLogic.UI;
using UnityEngine;

namespace Infrastructure.Services
{
    public class GameControlService 
    { 
        private PlayerStatsChangerService _playerStatsChangerService;
        GameControlService(PlayerStatsChangerService playerStatsChangerService)
        {
            _playerStatsChangerService = playerStatsChangerService;
        }
        public void UpgradeLevelUp(UpgradeItem upgradeItem)
        {
            switch (upgradeItem.upgradeType)
            {
                case UpgradeType.Damage:
                    UpgradeDamage(upgradeItem);
                    break;
                case UpgradeType.Health:
                    UpgradeHealth(upgradeItem);
                    break;
                case UpgradeType.FireRate:
                    UpgradeFireRate(upgradeItem);
                    break;
                case UpgradeType.WeaponType:
                    UpgradeWeapon(upgradeItem);
                    break;
                case UpgradeType.ModelType:
                    UpgradeBoatModel(upgradeItem);
                    break;
            }
        }

        public void UpgradeBoatModel(UpgradeItem upgradeItem)
        {
            Debug.Log("UpgradeBoatModel");
            _playerStatsChangerService.AddModelTypeUpgradeLevel(+1);
            Debug.Log($"UpgradeBoatModel {_playerStatsChangerService.GetModelTypeUpgradeLevel()}");
            _playerStatsChangerService.SetModelType((ModelType) upgradeItem.value[_playerStatsChangerService.GetModelTypeUpgradeLevel()]);
        }

        public void UpgradeWeapon(UpgradeItem upgradeItem)
        {
            Debug.Log("UpgradeBoatModel");
            _playerStatsChangerService.AddWeaponTypeUpgradeLevel(+1);
            Debug.Log($"UpgradeBoatModel {_playerStatsChangerService.GetWeaponTypeUpgradeLevel()}");
            _playerStatsChangerService.SetWeaponType(
                (WeaponType) upgradeItem.value[_playerStatsChangerService.GetWeaponTypeUpgradeLevel()]);
        }

        public void UpgradeFireRate(UpgradeItem upgradeItem)
        {
            int value = upgradeItem.value[_playerStatsChangerService.GetFireRateUpgradeLevel()];
            _playerStatsChangerService.AddFireRateUpgradeLevel(+1);
            _playerStatsChangerService.SetFireRateCoefficient(_playerStatsChangerService.GetFireRateCoefficient() * (100 + value) / 100f);
        }

        public void UpgradeHealth(UpgradeItem upgradeItem)
        {
            _playerStatsChangerService.AddMaxHealthUpgradeLevel(+1);
            _playerStatsChangerService.AddMaxHealth(upgradeItem.value[_playerStatsChangerService.GetMaxHealthUpgradeLevel()]);
            _playerStatsChangerService.SetHealth(_playerStatsChangerService.GetMaxHealth());
        }

        public void UpgradeDamage(UpgradeItem upgradeItem)
        {
            var value = upgradeItem.value[_playerStatsChangerService.GetWeaponDamageUpgradeLevel()];
            _playerStatsChangerService.SetWeaponDamageCoefficient(_playerStatsChangerService.GetWeaponDamageCoefficient() * (100 + value) / 100);
            _playerStatsChangerService.AddWeaponDamageUpgradeLevel(+1);
        }
    }
}