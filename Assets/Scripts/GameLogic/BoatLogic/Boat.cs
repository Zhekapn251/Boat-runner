using GameLogic.BoatUpgradeSystem;
using GameLogic.Entities;
using UnityEngine;
using Zenject;

namespace GameLogic.BoatLogic
{
    public class Boat: MonoBehaviour
    {
       
        public BoatStats Stats = new BoatStats();
        
        private int _coins;
        private int _health;
        private float _progressLevel;
        private IUpgradeSystem _upgradeSystem;

        [Inject]
        public void Construct(IUpgradeSystem upgradeSystem)
        {
            _upgradeSystem = upgradeSystem;
        }

        public void UpgradeHealth(int amount)
        {
            _upgradeSystem.ApplyUpgrade(this, new HealthUpgradeStrategy(amount));
        }

        public void UpgradeDamage(int damageAmount)
        {
            _upgradeSystem.ApplyUpgrade(this, new DamageUpgradeStrategy(damageAmount));
        }
        
        public void UpgradeWeapon(WeaponType weaponType)
        {
            _upgradeSystem.ApplyUpgrade(this, new WeaponUpgradeStrategy(weaponType));
        }
        
        public void UpgradeFireRate(float fireRateAmount)
        {
            _upgradeSystem.ApplyUpgrade(this, new FireRateUpgradeStrategy(fireRateAmount));
        }

        public void UpgradeBoatModel(ModelType modelType)
        {
            _upgradeSystem.ApplyUpgrade(this, new ModelUpgradeStrategy(modelType));
        }
        
    }
}