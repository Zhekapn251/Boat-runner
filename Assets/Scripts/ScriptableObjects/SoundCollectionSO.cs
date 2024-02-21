using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoundCollectionSO", menuName = "SoundCollectionSO")]
    public class SoundCollectionSO : ScriptableObject
    {
        [Header("Music")] 
        public SoundSO[] BackgroundMusic;

        [Header("SFX")] 
        public SoundSO[] CoinsPickup;
        public SoundSO[] EnemyDeath;
        public SoundSO[] GunShoot;
        public SoundSO[] HitTarget;
        public SoundSO[] Lose;
        public SoundSO[] PlayerHit;
        public SoundSO[] Win;
    }
}