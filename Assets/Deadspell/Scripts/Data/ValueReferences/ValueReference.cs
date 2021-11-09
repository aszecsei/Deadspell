using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.ValueReferences
{
    public class ValueReference<T> : SerializedScriptableObject
    {
        public T Value;
        
        public static implicit operator T(ValueReference<T> wrapper)
        {
            return wrapper.Value;
        }
    }
}