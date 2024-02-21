using GameLogic.BoatLogic;

namespace GameLogic.BoatUpgradeSystem
{
    public interface IUpgradeStrategy
    {
        void Upgrade(Boat boat);
    }
}