using GameLogic.Entities;

namespace GameLogic.UI
{
    public class PlayerStats
    {
        public int Coins = 1000;
        public int MaxHealth = 50;
        public int MaxHealthUpgradeLevel = 0;
        public int Health;
        public float WeaponDamageCoefficient = 1f;
        public int WeaponDamageUpgradeLevel = 0;
        public float FireRateCoefficient = 1f;
        public int FireRateUpgradeLevel = 0;
        public float LevelProgress = 0f;
        public WeaponType WeaponType = WeaponType.Gun;
        public int WeaponTypeUpgradeLevel = 0;
        public ModelType ModelType = ModelType.Model1;
        public int ModelTypeUpgradeLevel = 0;
        
    }
}