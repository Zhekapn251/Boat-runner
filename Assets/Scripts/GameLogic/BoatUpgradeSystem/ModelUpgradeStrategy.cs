using GameLogic.BoatLogic;
using GameLogic.Entities;

namespace GameLogic.BoatUpgradeSystem
{
    public class ModelUpgradeStrategy : IUpgradeStrategy
    {
        private ModelType _modelType;

        public ModelUpgradeStrategy(ModelType modelType)
        {
            _modelType = modelType;
        }

        public void Upgrade(Boat boat)
        {
            boat.Stats.ModelType = _modelType;
        }
    }
}