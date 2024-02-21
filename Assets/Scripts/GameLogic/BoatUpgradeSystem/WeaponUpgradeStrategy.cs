using GameLogic.BoatLogic;
using GameLogic.Entities;

namespace GameLogic.BoatUpgradeSystem
{
    public class WeaponUpgradeStrategy : IUpgradeStrategy
    {
        private WeaponType _weaponType;

        public WeaponUpgradeStrategy(WeaponType weaponType)
        {
            _weaponType = weaponType;
        }

        public void Upgrade(Boat boat)
        {
            boat.Stats.WeaponType = _weaponType;
        }
    }
}