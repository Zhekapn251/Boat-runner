using GameLogic.BoatLogic;

namespace GameLogic.BoatUpgradeSystem
{
    public interface IUpgradeSystem
    {
        void ApplyUpgrade(Boat boat, IUpgradeStrategy strategy);
    }
}