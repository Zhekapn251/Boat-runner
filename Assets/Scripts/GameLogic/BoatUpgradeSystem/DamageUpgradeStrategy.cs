using GameLogic.BoatLogic;

namespace GameLogic.BoatUpgradeSystem
{
    public class DamageUpgradeStrategy : IUpgradeStrategy
    {
        private int _amount;

        public DamageUpgradeStrategy(int amount)
        {
            _amount = amount;
        }

        public void Upgrade(Boat boat)
        {
            boat.Stats.WeaponDamage += _amount;
        }
    }
}