using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoundSO", menuName = "SoundSO", order = 0)]
    public class SoundSO : ScriptableObject
    {
        public AudioTypes AudioType;
        public AudioClip Clip;
        public bool Loop = false;
        public bool RandomizePitch = false;
        [Range(0f, 1f)] public float RandomPitchRangeModifier = 0.1f;
        [Range(0.1f, 2f)] public float Volume = 1f;
        [Range(0.1f, 3f)] public float Pitch = 1f;

    }
}