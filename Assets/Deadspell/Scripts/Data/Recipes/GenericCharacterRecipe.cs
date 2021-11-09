using System.Collections.Generic;
using Deadspell.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data.Recipes
{
    [CreateAssetMenu(menuName = "Deadspell/Characters/Generic Character")]
    public class GenericCharacterRecipe : SerializedScriptableObject
    {
        public string Name;
        
        public List<Race> Races;
        public Renderable Renderable = new Renderable
        {
            Color = default,
            Sprite = null,
        };
        public bool BlocksTile = true;
        public int VisionRange = 4;

        public enum MovementType
        {
            Static,
            Random,
            RandomWaypoint,
        }
        public MovementType Movement = MovementType.Static;
        public List<string> Quips = null;
        public Dictionary<AttributeType, int> Attributes = new Dictionary<AttributeType, int>();
        public Dictionary<Skill, int> Skills = new Dictionary<Skill, int>();
        public int? Level = null;
        public int? Health = null;
        public int? Mana = null;
        public List<ItemRecipe> Equipped = new List<ItemRecipe>();
        public List<ItemRecipe> Inventory = new List<ItemRecipe>();

        public LootTable LootTable;
        
        public Faction Faction;
        public string Gold;
        
        public VendorCategory Vendor = VendorCategory.None;

        public class Ability
        {
            public SpellRecipe Spell;
            public float Chance;
            public float Range;
            public float MinRange;
        }
        public List<Ability> Abilities = null;
        public List<Ability> OnDeath = null;
    }
}