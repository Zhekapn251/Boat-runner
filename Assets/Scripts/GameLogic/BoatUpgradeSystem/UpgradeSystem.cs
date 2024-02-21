using GameLogic.BoatLogic;

namespace GameLogic.BoatUpgradeSystem
{
    public class UpgradeSystem : IUpgradeSystem
    {
        public void ApplyUpgrade(Boat boat, IUpgradeStrategy strategy)
        {
            strategy.Upgrade(boat);
        }
    }
}