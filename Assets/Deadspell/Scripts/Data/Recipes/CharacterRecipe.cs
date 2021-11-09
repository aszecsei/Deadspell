using System;
using System.Collections.Generic;
using Deadspell.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using Attribute = Deadspell.Core.Attribute;

namespace Deadspell.Data.Recipes
{
    [CreateAssetMenu(menuName = "Deadspell/Characters/Named Character")]
    public class CharacterRecipe : Recipe
    {
        public string Name;

        public bool GenericMonster = false;
        [HideIf("GenericMonster")]
        public Race Race;
        [ShowIf("GenericMonster")]
        public CreatureType Type = CreatureType.Humanoid;
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
        public class CharacterNatural
        {
            public int? ArmorClass;
            public List<Attack> Attacks = new List<Attack>();

            public class Attack
            {
                public string Name;
                public int HitBonus;
                public string Damage;
            }
        }

        public CharacterNatural Natural = null;
        public LootTable LootTable;

        public class CharacterLight
        {
            public int Range;
            public Color Color;
        }

        public CharacterLight Light;
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