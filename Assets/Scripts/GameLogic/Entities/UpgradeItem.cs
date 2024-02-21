using System;
using UnityEngine;

namespace GameLogic.Entities
{
    [Serializable]
    public class UpgradeItem
    {
        public string itemName;
        public int[] cost;
        public string description;
        public Sprite[] image; 
        public int[] value;
        public UpgradeType upgradeType; 
        
    }
}