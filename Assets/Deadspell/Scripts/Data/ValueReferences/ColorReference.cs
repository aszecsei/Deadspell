using UnityEngine;

namespace Deadspell.ValueReferences
{
    [CreateAssetMenu(menuName = "Color")]
    public class ColorReference : ValueReference<Color>
    {
        public ColorReference()
        {
            Value = Color.black;
        }
    }
}