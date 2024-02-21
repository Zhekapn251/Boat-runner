using System;
using GameLogic.Entities;
using GameLogic.UI;

namespace Infrastructure.Services
{
    public class PlayerStatsChangerService
    {
        public event Action<int> OnCoinsChanged; 
        public event Action<int> OnHealthChanged;
        public event Action<float> OnProgressLevelChanged;
        public event Action<WeaponType> OnWeaponTypeChanged;
        public event Action<ModelType> OnModelTypeChanged;
        public event Action<float> OnWeaponDamageCoefficientChanged;
        public event Action<float> OnFireRateCoefficient;


        private  PlayerStats _playerStats;
        public PlayerStatsChangerService(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }
        
        public void ResetAllData()
        {
            _playerStats = new PlayerStats();
            UpdateAllData();
        }
        public void UpdateAllData()
        {
            _playerStats.Health = _playerStats.MaxHealth;
            OnCoinsChanged?.Invoke(_playerStats.Coins);
            OnHealthChanged?.Invoke(_playerStats.Health);
            OnProgressLevelChanged?.Invoke(_playerStats.LevelProgress);
            OnWeaponTypeChanged?.Invoke(_playerStats.WeaponType);
            OnModelTypeChanged?.Invoke(_playerStats.ModelType);
            OnWeaponDamageCoefficientChanged?.Invoke(_playerStats.WeaponDamageCoefficient);
            OnFireRateCoefficient?.Invoke(_playerStats.FireRateCoefficient);
        }
        public void ProgressLevelChanged(float levelProgress)
        {
            _playerStats.LevelProgress = levelProgress;
            OnProgressLevelChanged?.Invoke(levelProgress);
        }
        public int GetCoins() => _playerStats.Coins;
        public void AddCoins(int coins)
        {
            _playerStats.Coins += coins;
            OnCoinsChanged?.Invoke(_playerStats.Coins);
        }
        public void SubtractCoins(int coins) 
        {
            _playerStats.Coins -= coins;
            if (_playerStats.Coins < 0)
                _playerStats.Coins = 0;
            OnCoinsChanged?.Invoke(_playerStats.Coins);
        }
        public int GetMaxHealth() => _playerStats.MaxHealth;
        public void AddMaxHealth(int health)
        {
            _playerStats.MaxHealth += health;
        }
        
        public void AddMaxHealthUpgradeLevel(int level)
        {
            _playerStats.MaxHealthUpgradeLevel += level;
        }
        public void SetHealth(int health)
        {
            _playerStats.Health = health;
            OnHealthChanged?.Invoke(_playerStats.Health);
        }
        public int GetHealth() => _playerStats.Health;
        public void AddHealth(int health)
        {
            _playerStats.Health += health;
            OnHealthChanged?.Invoke(_playerStats.Health);
        }
        public void SubtractHealth(int health)
        {
            _playerStats.Health -= health;
            if (_playerStats.Health < 0)
                _playerStats.Health = 0;
            OnHealthChanged?.Invoke(_playerStats.Health);
        }
        public float GetWeaponDamageCoefficient() => _playerStats.WeaponDamageCoefficient;
        public void SetWeaponDamageCoefficient(float coefficient)
        {
            _playerStats.WeaponDamageCoefficient = coefficient;
            OnWeaponDamageCoefficientChanged?.Invoke(_playerStats.WeaponDamageCoefficient);
        }
        public void AddWeaponDamageUpgradeLevel(int level)
        {
            _playerStats.WeaponDamageUpgradeLevel += level;
        }
        public float GetFireRateCoefficient() => _playerStats.FireRateCoefficient;
        public void SetFireRateCoefficient(float coefficient)
        {
            _playerStats.FireRateCoefficient = coefficient;
            OnFireRateCoefficient?.Invoke(_playerStats.FireRateCoefficient);
        }
        public void AddFireRateUpgradeLevel(int level)
        {
            _playerStats.FireRateUpgradeLevel += level;
        }
        public float GetProgressLevel() => _playerStats.LevelProgress;
        public int GetUpgradeLevel(UpgradeType upgradeItemUpgradeType)
        {
            return upgradeItemUpgradeType switch
            {
                UpgradeType.Damage => GetWeaponDamageUpgradeLevel(),
                UpgradeType.Health => GetMaxHealthUpgradeLevel(),
                UpgradeType.FireRate => GetFireRateUpgradeLevel(),
                UpgradeType.WeaponType => GetWeaponTypeUpgradeLevel(),
                UpgradeType.ModelType => GetModelTypeUpgradeLevel(),
                _ => 0
            };
        }
        public void AddWeaponTypeUpgradeLevel(int level) => 
            _playerStats.WeaponTypeUpgradeLevel += level;
        public int GetModelTypeUpgradeLevel() => 
            _playerStats.ModelTypeUpgradeLevel;
        public void AddModelTypeUpgradeLevel(int level) => 
            _playerStats.ModelTypeUpgradeLevel += level;
        public WeaponType GetWeaponType() => _playerStats.WeaponType;
        public void SetWeaponType(WeaponType weaponType)
        {
            _playerStats.WeaponType = weaponType;
            OnWeaponTypeChanged?.Invoke(_playerStats.WeaponType);
        }
        public void SetModelType(ModelType modelType)
        {
            _playerStats.ModelType = modelType;
            OnModelTypeChanged?.Invoke( _playerStats.ModelType);
        }
        public int GetWeaponTypeUpgradeLevel() => _playerStats.WeaponTypeUpgradeLevel;
        public int GetFireRateUpgradeLevel() => _playerStats.FireRateUpgradeLevel;
        public int GetWeaponDamageUpgradeLevel() => _playerStats.WeaponDamageUpgradeLevel;
        public int GetMaxHealthUpgradeLevel() => _playerStats.MaxHealthUpgradeLevel;
    }
}