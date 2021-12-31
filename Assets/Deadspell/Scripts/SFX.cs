using System;
using System.Collections.Generic;
using Deadspell.Data;
using Deadspell.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell
{
    [System.Serializable]
    public class SFX
    {
        [LabelText("SFX Type")]
        [LabelWidth(100)]
        [OnValueChanged(nameof(SFXChange))]
        [InlineButton(nameof(PlaySFX))]
        public SFXManager.SFXType SfxType = SFXManager.SFXType.UI;

        [LabelText("$_sfxLabel")]
        [LabelWidth(100)]
        [ValueDropdown(nameof(SFXType))]
        [OnValueChanged(nameof(SFXChange))]
        [InlineButton(nameof(SelectSFX))]
        public SFXClip SfxToPlay;
        private string _sfxLabel = "SFX";

        [SerializeField]
        private bool _showSettings;
        [SerializeField]
        private bool _editSettings;

        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [SerializeField]
        [ShowIf(nameof(_showSettings))]
        [EnableIf(nameof(_editSettings))]
        private SFXClip _sfxBase;
        
        [Title("Audio Source")]
        [SerializeField]
        [ShowIf(nameof(_showSettings))]
        [EnableIf(nameof(_editSettings))]
        private bool _waitToPlay = true;
        
        [SerializeField]
        [ShowIf(nameof(_showSettings))]
        [EnableIf(nameof(_editSettings))]
        private bool _useDefault = true;
        
        [SerializeField]
        [DisableIf(nameof(_useDefault))]
        [ShowIf(nameof(_showSettings))]
        [EnableIf(nameof(_editSettings))]
        private AudioSource _audioSource;

        private void SFXChange()
        {
            _sfxLabel = $"{SfxType.ToString()} SFX";
            _sfxBase = SfxToPlay;
        }

        private void SelectSFX()
        {
            UnityEditor.Selection.activeObject = SfxToPlay;
        }

        private List<SFXClip> SFXType()
        {
            return SfxType switch
            {
                SFXManager.SFXType.UI => SFXManager.Instance.UiSfx,
                SFXManager.SFXType.Ambient => SFXManager.Instance.AmbientSfx,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public void PlaySFX()
        {
            SFXManager.PlaySfx(SfxToPlay, _waitToPlay, _useDefault ? null : _audioSource);
        }
    }
}