using UnityEngine;

namespace Deadspell.ValueReferences
{
    [CreateAssetMenu(menuName = "Deadspell/Layer")]
    public class LayerReference : ValueReference<int>
    {
        public LayerReference()
        {
            Value = 1;
        }
    }
}