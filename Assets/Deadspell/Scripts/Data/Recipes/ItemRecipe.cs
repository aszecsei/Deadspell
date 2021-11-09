using System;
using System.Collections.Generic;
using Deadspell.Core;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.TypeSearch;

namespace Deadspell.Data.Recipes
{
    [CreateAssetMenu(menuName = "Deadspell/Item")]
    public class ItemRecipe : Recipe
    {
        public string Name;

        public Renderable Renderable = new Renderable
        {
            Color = default,
            Sprite = null,
        };

        public Consumable Consumable = null;
        public Weapon Weapon = null;
        public Wearable Wearable = null;
        public Weight Weight = null;
        public Value Value = null;
        public VendorCategory Category = VendorCategory.None;
        public MagicItem Magic = null;
        public Dictionary<AttributeType, int> Attributes = null;
    }

    public class Consumable
    {
        public List<Effect> Effects = new List<Effect>();
        public Target Target = Target.Self;
        [HideIf("Target", Data.Target.Self)]
        public float Range = 0;
        public int? Charges = 1;
    }

    public class MagicItem
    {
        public MagicItemClass Class;
        public MagicItemNaming Naming;
        [ShowIf("Naming", MagicItemNaming.Default)]
        public string UnidentifiedName;
        public bool Cursed;
    }
    
    public class Weapon
    {
        public enum WeaponRangeType
        {
            Melee,
            Ranged,
            Thrown,
        }

        public enum WeaponHandedness
        {
            OneHanded,
            TwoHanded,
            Versatile,
        }

        public enum WeaponHeaviness
        {
            Light,
            Normal,
            Heavy,
        }
        
        [EnumToggleButtons]
        public WeaponRangeType RangeType;

        [EnumToggleButtons]
        public WeaponHandedness Handedness;

        [EnumToggleButtons]
        public WeaponHeaviness Heaviness = WeaponHeaviness.Normal;
        
        [HideIf("RangeType", WeaponRangeType.Melee)]
        public int Range = 3;

        public AttributeType Attribute;

        [HideIf("Handedness", WeaponHandedness.TwoHanded)]
        public string OneHandedDamage;

        [HideIf("Handedness", WeaponHandedness.OneHanded)]
        public string TwoHandedDamage;
        public int HitBonus;
    }
    
    public class Wearable
    {
        public EquipmentSlot Slot;
        public ArmorType ArmorType;
        public float ArmorClass;
    }
    
    public class Weight
    {
        public float ItemWeight;
    }
    
    public class Value
    {
        public float ItemValue;
    }
    
    public class ItemAttributeBonus
    {
        [InlineProperty]
        [HideLabel]
        public Attributes.Modifier Bonus;
    }
}