using System.Collections.Generic;
using GameLogic.Entities;
using UnityEngine;

namespace GameLogic.Configs
{
    [CreateAssetMenu (fileName = "UpgradeItemSO", menuName = "Configs/UpgradeItemSO")]
    public class UpgradeItemSO : ScriptableObject
    {
        public List<UpgradeItem> items;
    }
}