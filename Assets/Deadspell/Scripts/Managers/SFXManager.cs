using System.Collections.Generic;
using Deadspell.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Managers
{
    public class SFXManager : MonoBehaviour
    {
        private static SFXManager _instance;

        public static SFXManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SFXManager>();
                }

                return _instance;
            }
        }

        [HorizontalGroup("AudioSource")] [SerializeField]
        private AudioSource _defaultAudioSource;

        [TabGroup("UI")]
        [AssetList(Path = "/Deadspell/Audio/UI", AutoPopulate = true)]
        public List<SFXClip> UiSfx;
        [TabGroup("Ambient")]
        [AssetList(Path = "/Deadspell/Audio/Ambient", AutoPopulate = true)]
        public List<SFXClip> AmbientSfx;

        public static void PlaySfx(SFXClip sfx, bool waitToFinish = true, AudioSource audioSource = null)
        {
            audioSource ??= Instance._defaultAudioSource;

            if (audioSource == null)
            {
                Debug.LogError("No default audio source specified");
                return;
            }

            if (!audioSource.isPlaying || !waitToFinish)
            {
                audioSource.clip = sfx.Clip;
                audioSource.volume = sfx.Volume + Random.Range(-sfx.VolumeVariation, sfx.VolumeVariation);
                audioSource.pitch = sfx.Pitch + Random.Range(-sfx.PitchVariation, sfx.PitchVariation);
                audioSource.Play();
            }
        }

        [HorizontalGroup("AudioSource")]
        [ShowIf("@_defaultAudioSource == null")]
        [GUIColor(1f, 0.5f, 0.5f, 1f)]
        [Button]
        private void AddAudioSource()
        {
            _defaultAudioSource = gameObject.GetComponent<AudioSource>();

            if (_defaultAudioSource == null)
            {
                _defaultAudioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        public enum SFXType
        {
            UI,
            Ambient,
        }
    }
}