using Deadspell.ValueReferences;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "Deadspell/Tile")]
    public class Tile : SerializedScriptableObject
    {
        public Sprite Sprite;
        public ColorReference Color;
    }
}