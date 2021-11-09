using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "Deadspell/Magic School")]
    public class MagicSchool : SerializedScriptableObject
    {
        public string Name;
        
        [Title("Description", bold: false)]
        [HideLabel]
        [MultiLineProperty(10)]
        public string Description;
    }
}