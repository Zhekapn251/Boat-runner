using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Infrastructure.Interfaces
{
    public class AudioService: MonoBehaviour
    {
        [SerializeField] private SoundCollectionSO soundCollectionSO;
        [SerializeField] private AudioMixerGroup musicMixerGroup;
        [SerializeField] private AudioMixerGroup sfxMixerGroup;
        
        private AudioSource _currentMusicSource;
        private AudioSource _currentSFXSource;
        
        
        private float _masterVolume = 1f;
        public void PlayCoinsPickup() => PlayRandomSound(soundCollectionSO.CoinsPickup);
        public void PlayEnemyDeath() => PlayRandomSound(soundCollectionSO.EnemyDeath);
        public void PlayGunShoot() => PlayRandomSound(soundCollectionSO.GunShoot);
        public void PlayHitTarget() => PlayRandomSound(soundCollectionSO.HitTarget);
        public void PlayLose() => PlayRandomSound(soundCollectionSO.Lose);
        public void PlayPlayerHit() => PlayRandomSound(soundCollectionSO.PlayerHit);
        public void PlayWin() => PlayRandomSound(soundCollectionSO.Win);

        public void PlayMusic() => PlayRandomSound(soundCollectionSO.BackgroundMusic);

        private void PlayRandomSound(SoundSO[] sounds) 
        {
            if (sounds != null && sounds.Length > 0) {
                int randomIndex = Random.Range(0, sounds.Length);
                SoundToPlay(sounds[randomIndex]);
            }
            else {
                Debug.LogWarning("No sounds in array");
            }
        }
        
        private void SoundToPlay(SoundSO soundSO) {
            AudioClip clip = soundSO.Clip;
            float pitch = soundSO.Pitch;
            float volume = soundSO.Volume * _masterVolume;
        
            bool loop = soundSO.Loop;
            AudioMixerGroup mixerGroup; 
            switch (soundSO.AudioType) {
                case AudioTypes.Music:
                    mixerGroup = musicMixerGroup;
                    //Debug.Log("Music");
                    break;
                case AudioTypes.SFX:
                    mixerGroup = sfxMixerGroup;
                    //Debug.Log("SFX");
                    break;
                default:
                    throw new Exception("No Audio Type");
            
            }

            if (soundSO.RandomizePitch) {
                float randomPitchModifier = Random.Range(-soundSO.RandomPitchRangeModifier, soundSO.RandomPitchRangeModifier);
                pitch = soundSO.Pitch + randomPitchModifier;
            }

            PlaySound(clip, pitch, volume, loop, mixerGroup);
        }
        private void PlaySound(AudioClip clip, float pitch, float volume, bool loop, AudioMixerGroup mixerGroup)
        {
            GameObject soundObject = new GameObject("Temp Audio Source");
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.pitch = pitch;
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.outputAudioMixerGroup = mixerGroup;
        
            if (mixerGroup == musicMixerGroup)
            {
                if (_currentMusicSource != null)
                {
                    _currentMusicSource.Stop(); 
                    Destroy(_currentMusicSource.gameObject); 
                
                }
                _currentMusicSource = audioSource;
                _currentMusicSource.Play();
            }
            else if (mixerGroup == sfxMixerGroup)
            {
                _currentSFXSource = audioSource;
                _currentSFXSource.Play();
            
            }

        
            if (!loop) { Destroy(soundObject, clip.length ); }
        }
        
    }
}