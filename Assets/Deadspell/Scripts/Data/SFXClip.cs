using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "New SFX Clip", fileName = "NewSFXClip")]
    public class SFXClip : SerializedScriptableObject
    {
        [Space]
        [Required]
        public AudioClip Clip;

        [Title("Clip Settings")]
        [Range(0f, 1f)]
        public float Volume = 1f;
        [Range(0f, 0.2f)]
        public float VolumeVariation = 0.05f;
        [Range(0f, 2f)]
        public float Pitch = 1f;
        [Range(0f, 0.2f)]
        public float PitchVariation = 0.05f;
    }
}