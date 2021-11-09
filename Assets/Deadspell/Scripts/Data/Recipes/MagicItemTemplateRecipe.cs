using System;
using System.Collections.Generic;
using System.IO;
using Deadspell.Core;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Deadspell.Data.Recipes
{
    [CreateAssetMenu(menuName = "Deadspell/Magic Item Template")]
    public class MagicItemTemplateRecipe : Recipe
    {
        [MenuItem("Assets/Deadspell/Item to Magic Item Template", true)]
        private static bool CreateMagicItemTemplateValidator()
        {
            var activeObject = Selection.activeObject;
            
            // Make sure it is an item recipe
            if ((activeObject == null) || !(activeObject is ItemRecipe))
            {
                return false;
            }
            
            // Make sure it is a persistent asset
            var assetPath = AssetDatabase.GetAssetPath(activeObject);
            if (string.IsNullOrEmpty(assetPath))
            {
                return false;
            }

            return true;
        }
        [MenuItem("Assets/Create/Deadspell/Magic Item Template From Item")]
        private static void CreateMagicItemTemplateValidator(MenuCommand command)
        {
            // We've already validated this path and thus know these calls are safe
            var activeObject = Selection.activeObject as ItemRecipe;
            
            var assetPath = AssetDatabase.GetAssetPath(activeObject);
            assetPath = assetPath.Replace($"{activeObject!.name}.asset", $"{activeObject!.name} Template.asset");
            assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);

            var newItem = ScriptableObject.CreateInstance<MagicItemTemplateRecipe>();
            newItem.name = $"{activeObject!.name} Template";
            newItem.Base = activeObject;
            
            AssetDatabase.CreateAsset(newItem, assetPath);
            AssetDatabase.SaveAssets();

            Selection.activeObject = newItem;
        }
        
        public ItemRecipe Base;

        public MagicItemTemplate MagicItemTemplate = new MagicItemTemplate();

        [Button("Generate Magic Items", ButtonSizes.Large)]
        public void GenerateMagicItems()
        {
            // Remove all existing item children
            var path = AssetDatabase.GetAssetPath(this);
            if (!string.IsNullOrEmpty(path))
            {
                var items = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);
                foreach (var item in items)
                {
                    AssetDatabase.RemoveObjectFromAsset(item);
                }
            }

            foreach (ItemRecipe magicItem in MagicItemTemplate.GetMagicItems(Base))
            {
                if (!string.IsNullOrEmpty(path))
                {
                    AssetDatabase.AddObjectToAsset(magicItem, this);
                }
                AssetDatabase.SaveAssets();
            }
        }
    }

    [Serializable]
    public class MagicItemTemplate
    {
        public string UnidentifiedName = "Unidentified $Name$";
        
        [HorizontalGroup]
        public int MinBonus = 1;

        [HorizontalGroup]
        public int MaxBonus = 5;

        public bool IncludeCursed = true;

        public IEnumerable<ItemRecipe> GetMagicItems(ItemRecipe baseItem)
        {
            for (int bonus = MinBonus; bonus <= MaxBonus; bonus++)
            {
                var newItem = ScriptableObject.CreateInstance<ItemRecipe>();

                newItem.Name = $"{baseItem.Name} +{bonus}";
                newItem.name = newItem.Name;
                
                newItem.Renderable = baseItem.Renderable;
                newItem.Consumable = baseItem.Consumable;
                if (baseItem.Weapon != null)
                {
                    newItem.Weapon = new Weapon
                    {
                        Attribute = baseItem.Weapon.Attribute,
                        Handedness = baseItem.Weapon.Handedness,
                        Heaviness = baseItem.Weapon.Heaviness,
                        HitBonus = baseItem.Weapon.HitBonus + bonus,
                        OneHandedDamage = $"{baseItem.Weapon.OneHandedDamage}+{bonus}",
                        TwoHandedDamage = $"{baseItem.Weapon.TwoHandedDamage}+{bonus}",
                        RangeType = baseItem.Weapon.RangeType,
                        Range = baseItem.Weapon.Range,
                    };
                }

                if (baseItem.Wearable != null)
                {
                    newItem.Wearable = new Wearable
                    {
                        ArmorClass = baseItem.Wearable.ArmorClass + bonus,
                        ArmorType = baseItem.Wearable.ArmorType,
                        Slot = baseItem.Wearable.Slot,
                    };
                }

                newItem.Weight = baseItem.Weight;
                if (baseItem.Value != null)
                {
                    newItem.Value = new Value
                    {
                        ItemValue = baseItem.Value.ItemValue * (bonus + 1),
                    };
                }

                newItem.Category = baseItem.Category;
                newItem.Magic = new MagicItem
                {
                    Class = MagicItemClass.Common,
                    Cursed = false,
                    Naming = MagicItemNaming.Default,
                    UnidentifiedName = UnidentifiedName.Replace("$Name$", baseItem.Name),
                };
                newItem.Attributes = baseItem.Attributes;

                yield return newItem;
                
                if (IncludeCursed)
                {
                    var cursedItem = ScriptableObject.CreateInstance<ItemRecipe>();

                    cursedItem.Name = $"Cursed {baseItem.Name} -{bonus}";
                    cursedItem.name = cursedItem.Name;
                    
                    cursedItem.Renderable = baseItem.Renderable;
                    cursedItem.Consumable = baseItem.Consumable;
                    if (baseItem.Weapon != null)
                    {
                        cursedItem.Weapon = new Weapon
                        {
                            Attribute = baseItem.Weapon.Attribute,
                            Handedness = baseItem.Weapon.Handedness,
                            Heaviness = baseItem.Weapon.Heaviness,
                            HitBonus = baseItem.Weapon.HitBonus + bonus,
                            OneHandedDamage = $"{baseItem.Weapon.OneHandedDamage}-{bonus}",
                            TwoHandedDamage = $"{baseItem.Weapon.TwoHandedDamage}-{bonus}",
                            RangeType = baseItem.Weapon.RangeType,
                            Range = baseItem.Weapon.Range,
                        };
                    }

                    if (baseItem.Wearable != null)
                    {
                        cursedItem.Wearable = new Wearable
                        {
                            ArmorClass = baseItem.Wearable.ArmorClass - bonus,
                            ArmorType = baseItem.Wearable.ArmorType,
                            Slot = baseItem.Wearable.Slot,
                        };
                    }

                    cursedItem.Weight = baseItem.Weight;
                    if (baseItem.Value != null)
                    {
                        cursedItem.Value = new Value
                        {
                            ItemValue = baseItem.Value.ItemValue / (bonus + 1),
                        };
                    }

                    cursedItem.Category = baseItem.Category;
                    cursedItem.Magic = new MagicItem
                    {
                        Class = MagicItemClass.Common,
                        Cursed = true,
                        Naming = MagicItemNaming.Default,
                        UnidentifiedName = UnidentifiedName.Replace("$Name$", baseItem.Name),
                    };
                    cursedItem.Attributes = baseItem.Attributes;

                    yield return cursedItem;
                }
            }
        }
    }
}