using System.Collections.Generic;
using Deadspell.Core;
using Deadspell.Data.Recipes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "Deadspell/Race")]
    public class Race : SerializedScriptableObject
    {
        public string Name;
        public CreatureType Type = CreatureType.Humanoid;
        
        [Title("Description", bold: false)]
        [HideLabel]
        [MultiLineProperty(10)]
        public string Description;

        public Dictionary<AttributeType, int> AttributeBonuses = new Dictionary<AttributeType, int>();
        public float WalkingSpeed = 6;
        public float FlyingSpeed = 0;
        public float SwimmingSpeed = 3;
        public List<CharacterTraitRecipe> Traits = new List<CharacterTraitRecipe>();
        public List<Language> Languages = new List<Language>();
    }
}