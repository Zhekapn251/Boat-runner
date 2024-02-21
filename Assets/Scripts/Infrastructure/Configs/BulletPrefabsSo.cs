using GameLogic.Entities;
using UnityEngine;

namespace GameLogic.Configs
{
    [CreateAssetMenu(fileName = "BulletPrefabsSo", menuName = "Configs/BulletPrefabsSo")]
    public class BulletPrefabsSo: ScriptableObject
    {
        public BulletPrefabStructure[] BulletPrefabs;
    }
}