using GameLogic.BoatLogic;

namespace GameLogic.BoatUpgradeSystem
{
    public class FireRateUpgradeStrategy : IUpgradeStrategy
    {
        private float _amount;

        public FireRateUpgradeStrategy(float amount)
        {
            _amount = amount;
        }

        public void Upgrade(Boat boat)
        {
            boat.Stats.FireRate += _amount;
        }
    }
}