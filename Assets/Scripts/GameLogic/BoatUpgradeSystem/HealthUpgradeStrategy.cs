using GameLogic.BoatLogic;
using UnityEngine;

namespace GameLogic.BoatUpgradeSystem
{
    public class HealthUpgradeStrategy: IUpgradeStrategy
    {
        private int _amount; // 

        public HealthUpgradeStrategy(int amount)
        {
            _amount = amount;
        }

        public void Upgrade(Boat boat)
        {
            boat.Stats.MaxHealth += _amount;
            Debug.Log($"Attack upgraded by {_amount}. New level: {boat.Stats.MaxHealth}");
        }
        
        
    }
}