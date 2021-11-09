using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "Deadspell/Language")]
    public class Language : SerializedScriptableObject
    {
        public string Name;
    }
}