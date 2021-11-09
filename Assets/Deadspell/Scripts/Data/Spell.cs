using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "Deadspell/Spell")]
    public class Spell : SerializedScriptableObject
    {
        public string Name;
        public MagicSchool School;
        
        [Title("Description", bold: false)]
        [HideLabel]
        [MultiLineProperty(10)]
        public string Description;
        
        public int ManaCost;
        public Target Target;
        public float Range;
        public List<Effect> Effects = new List<Effect>();
    }
}