using GameLogic.Entities;
using UnityEngine;

namespace GameLogic.Configs
{
    [CreateAssetMenu(fileName = "BoatViewSo", menuName = "Configs/BoatViewSo")]
    public class BoatViewSo: ScriptableObject
    {
        public BoatViewStructure[] BoatViewStructures;
    }
}