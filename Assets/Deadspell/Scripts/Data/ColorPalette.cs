using System.Collections.Generic;
using Deadspell.ValueReferences;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "Deadspell/Color Palette")]
    public class ColorPalette : SerializedScriptableObject
    {
        public Dictionary<char, ColorReference> Colors;
    }
}