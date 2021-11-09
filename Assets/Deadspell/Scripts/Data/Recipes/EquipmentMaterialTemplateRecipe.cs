using System;
using System.Collections.Generic;
using Deadspell.Core;
using Deadspell.ValueReferences;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Deadspell.Data.Recipes
{
    [CreateAssetMenu(menuName = "Deadspell/Equipment Material Template")]
    public class EquipmentMaterialTemplateRecipe : Recipe
    {
        public string Name = "$Name$";
        public EquipmentSlot ArmorToGenerate = EquipmentSlot.Armor;

        public Dictionary<EquipmentSlot, string> ArmorNames = new Dictionary<EquipmentSlot, string>
        {
            { EquipmentSlot.Torso, "Armor" },
            { EquipmentSlot.Legs, "Pants" },
            { EquipmentSlot.Feet, "Boots" },
            { EquipmentSlot.Head, "Helm" },
            { EquipmentSlot.Hands, "Gloves" },
        };
        
        [EnumToggleButtons]
        public ArmorType ArmorType;
        public float TotalArmorClass = 10;

        public Dictionary<EquipmentSlot, float> ArmorClassSplit = new Dictionary<EquipmentSlot, float>
        {
            { EquipmentSlot.Torso, 3 },
            { EquipmentSlot.Legs, 3 },
            { EquipmentSlot.Feet, 1 },
            { EquipmentSlot.Head, 2 },
            { EquipmentSlot.Hands, 1 },
        };

        public float TotalWeight = 10;
        public Dictionary<EquipmentSlot, float> WeightSplit = new Dictionary<EquipmentSlot, float>
        {
            { EquipmentSlot.Torso, 7 },
            { EquipmentSlot.Legs, 7 },
            { EquipmentSlot.Feet, 1 },
            { EquipmentSlot.Head, 4 },
            { EquipmentSlot.Hands, 1 },
        };

        public Sprite Sprite;
        public Dictionary<EquipmentSlot, ColorReference> ArmorColors = new Dictionary<EquipmentSlot, ColorReference>
        {
            { EquipmentSlot.Torso, default },
            { EquipmentSlot.Legs, default },
            { EquipmentSlot.Feet, default },
            { EquipmentSlot.Head, default },
            { EquipmentSlot.Hands, default },
        };

        public float Value;
        public VendorCategory VendorCategory;

        public bool GenerateMagicItems;
        [ShowIf("GenerateMagicItems")]
        public MagicItemTemplate MagicItemTemplate;

        public IEnumerable<ItemRecipe> GetArmorPieces()
        {
            foreach (EquipmentSlot slot in Enum.GetValues(typeof(EquipmentSlot)))
            {
                if ((slot & (slot - 1)) == 0 && slot != EquipmentSlot.None)
                {
                    if (!ArmorToGenerate.HasFlag(slot))
                    {
                        continue;
                    }

                    var newItem = ScriptableObject.CreateInstance<ItemRecipe>();

                    newItem.Name = Name.Replace("$Name$", ArmorNames[slot]);
                    newItem.name = newItem.Name;

                    newItem.Renderable = new Renderable
                    {
                        Sprite = Sprite,
                        Color = ArmorColors[slot],
                    };
                    
                    float totalArmorClassSplit = 0;
                    foreach (float acSplit in ArmorClassSplit.Values)
                    {
                        totalArmorClassSplit += acSplit;
                    }
                    float acPercentage = ArmorClassSplit[slot] / totalArmorClassSplit;
                    
                    newItem.Wearable = new Wearable
                    {
                        ArmorClass = TotalArmorClass * acPercentage,
                        ArmorType = ArmorType,
                        Slot = slot,
                    };

                    float totalWeightSplit = 0;
                    foreach (float weightSplit in WeightSplit.Values)
                    {
                        totalWeightSplit += weightSplit;
                    }
                    float weightPercentage = WeightSplit[slot] / totalWeightSplit;
                    newItem.Weight = new Weight
                    {
                        ItemWeight = TotalWeight * weightPercentage,
                    };

                    newItem.Value = new Value
                    {
                        ItemValue = Value,
                    };

                    newItem.Category = VendorCategory;

                    yield return newItem;
                    
                    if (GenerateMagicItems)
                    {
                        foreach (var magicItem in MagicItemTemplate.GetMagicItems(newItem))
                        {
                            yield return magicItem;
                        }
                    }
                }
            }
        }

        [Button("Generate Armor Pieces", ButtonSizes.Large)]
        public void GenerateArmorPieces()
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

            foreach (ItemRecipe magicItem in GetArmorPieces())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    AssetDatabase.AddObjectToAsset(magicItem, this);
                }
                AssetDatabase.SaveAssets();
            }
        }
    }
}