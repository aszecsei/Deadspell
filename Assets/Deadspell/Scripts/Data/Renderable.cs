using Deadspell.ValueReferences;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    public class Renderable
    {
        public Sprite Sprite;
        [InlineEditor]
        public ColorReference Color;
    }
}