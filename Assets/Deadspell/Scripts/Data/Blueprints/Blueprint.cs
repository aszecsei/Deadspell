using System;
using System.Collections.Generic;
using Deadspell.Components;
using Deadspell.Core;
using Deadspell.Map;
using Deadspell.ValueReferences;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data.Blueprints
{
    [CreateAssetMenu(menuName = "Deadspell/Blueprint")]
    public class Blueprint : SerializedScriptableObject
    {
        public Blueprint InheritsFrom;

        public List<BlueprintComponent> Components = new List<BlueprintComponent>();

        [ShowInInspector]
        [ReadOnly]
        public List<BlueprintComponent> AllComponents
        {
            get
            {
                List<BlueprintComponent> result = new List<BlueprintComponent>(Components);
                Blueprint toAdd = InheritsFrom;
                while (toAdd != null)
                {
                    for (int i = toAdd.Components.Count - 1; i >= 0; i--)
                    {
                        var component = toAdd.Components[i];
                        
                        bool alreadyExists = false;
                        foreach (var existingComponent in result)
                        {
                            if (component.GetType() == existingComponent.GetType())
                            {
                                alreadyExists = true;
                                break;
                            }
                        }

                        if (!alreadyExists)
                        {
                            result.Insert(0, component);
                        }
                    }

                    toAdd = toAdd.InheritsFrom;
                }

                return result;
            }
        }
    }

    [Serializable]
    public abstract class BlueprintComponent
    {
        public virtual void AddToEntity(GameEntity entity) {}
    }
    
    [Serializable]
    public class Display : BlueprintComponent
    {
        public string DisplayName;
        public Sprite Sprite;
        public ColorReference Color;
        public LayerReference RenderLayer;

        public override void AddToEntity(GameEntity entity)
        {
            entity.AddRenderable(TileCache.GetTile(Sprite, Color), RenderLayer);
            entity.AddName(DisplayName);
        }
    }

    [Serializable]
    public class Physics : BlueprintComponent
    {
        public bool BlocksTile = false;
        public bool BlocksVisibility = false;
        public float Weight = 0;

        public override void AddToEntity(GameEntity entity)
        {
            entity.isBlocksTile = BlocksTile;
            entity.isOpaque = BlocksVisibility;
        }
    }

    [Serializable]
    public class Description : BlueprintComponent
    {
        [HideLabel]
        [TextArea]
        public string Value;
    }

    [Serializable]
    public class Hidden : BlueprintComponent
    {
        public bool IsHidden = true;

        public override void AddToEntity(GameEntity entity)
        {
            entity.isHidden = IsHidden;
        }
    }

    [Serializable]
    public class Door : BlueprintComponent
    {
        public bool IsDoor = true;
    }

    [Serializable]
    public class Light : BlueprintComponent
    {
        public int Range = 4;
        [InlineEditor]
        public ColorReference Color;
    }

    [Serializable]
    public class EntityTrigger : BlueprintComponent
    {
        public List<Effect> Effects = new List<Effect>();
        public bool SingleActivation = false;
    }
    
    [Serializable]
    public class Consumable : BlueprintComponent
    {
        public List<Effect> Effects = new List<Effect>();
        public Target Target = Target.Self;

        [HideIf(nameof(Target), Data.Target.Self)]
        public float Range = 0f;

        public bool HasCharges = true;
        [ShowIf(nameof(HasCharges))]
        public int Charges = 1;
    }

    [Serializable]
    public class MagicItem : BlueprintComponent
    {
        public MagicItemClass Class;
        public MagicItemNaming Naming;
        [ShowIf("Naming", MagicItemNaming.Default)]
        public string UnidentifiedName;
        public bool Cursed;
    }
    
    [Serializable]
    public class Weapon : BlueprintComponent
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
    
    [Serializable]
    public class Wearable : BlueprintComponent
    {
        public EquipmentSlot Slot;
        public ArmorType ArmorType;
        public float ArmorClass;
    }

    [Serializable]
    public class AttributeBonus : BlueprintComponent
    {
        [HideLabel]
        public Attributes.Modifier Bonus;
    }

    [Serializable]
    public class Commerce : BlueprintComponent
    {
        public float Value;
    }

    [Serializable]
    public class Brain : BlueprintComponent
    {
        public Demeanor Demeanor = Demeanor.Neutral;
        public MovementType MovementType = MovementType.Static;
        public List<FactionLoyalty> Factions = new List<FactionLoyalty>();

        public override void AddToEntity(GameEntity entity)
        {
            // TODO: Demeanor
            // TODO: Movement type
            entity.AddFaction(Factions);
        }
    }

    [Serializable]
    public class Awareness : BlueprintComponent
    {
        public List<(Sense, int)> Senses = new List<(Sense, int)>();
    }

    [Serializable]
    public class Movement : BlueprintComponent
    {
        public float WalkingSpeed = 6;
        public float FlyingSpeed = 0;
        public float SwimmingSpeed = 3;
    }

    [Serializable]
    public class Quipping : BlueprintComponent
    {
        public List<string> Quips = new List<string>();
    }

    [Serializable]
    public class Stats : BlueprintComponent
    {
        public Dictionary<AttributeType, int> Attributes = new Dictionary<AttributeType, int>();
        public Dictionary<Skill, int> Skills = new Dictionary<Skill, int>();
        public int? Level = null;
        public int? Health = null;
        public int? Mana = null;

        public override void AddToEntity(GameEntity entity)
        {
            Attributes attr = new Attributes();
            {
                void AssignAttribute(AttributeType type, Deadspell.Core.Attribute attribute)
                {
                    if (Attributes.TryGetValue(type, out var attrOverride))
                    {
                        attribute.Base = attrOverride;
                    }
                }

                AssignAttribute(AttributeType.Might, attr.Might);
                AssignAttribute(AttributeType.Agility, attr.Agility);
                AssignAttribute(AttributeType.Vitality, attr.Vitality);
                
                AssignAttribute(AttributeType.Intelligence, attr.Intelligence);
                AssignAttribute(AttributeType.Wits, attr.Wits);
                AssignAttribute(AttributeType.Resolve, attr.Resolve);
                
                AssignAttribute(AttributeType.Faith, attr.Faith);
                AssignAttribute(AttributeType.Insight, attr.Insight);
                AssignAttribute(AttributeType.Conviction, attr.Conviction);
                
                AssignAttribute(AttributeType.Presence, attr.Presence);
                AssignAttribute(AttributeType.Manipulation, attr.Manipulation);
                AssignAttribute(AttributeType.Composure, attr.Composure);
                
                AssignAttribute(AttributeType.Luck, attr.Luck);
            }
            entity.AddAttributes(attr);

            Skills skills = new Skills();
            foreach (var sk in Skills)
            {
                skills.SkillValues[sk.Key] = sk.Value;
            }
            entity.AddSkills(skills);

            int level = Level ?? 1;
            int health = Health ?? GameSystem.NpcHealth(attr.Vitality, level);
            int mana = Mana ?? GameSystem.ManaAtLevel(attr.Wits, level);
            
            entity.AddStats(
                new StatsComponent.Pool(health),
                new StatsComponent.Pool(mana),
                0,
                level,
                0,
                0,
                false
                );
            entity.isStatsDirty = true;
        }
    }

    [Serializable]
    public class NaturalCombat : BlueprintComponent
    {
        public struct NaturalAttack
        {
            public string Name;
            public int HitBonus;
            public string Damage;
        }
        
        public int ArmorClass = 10;
        public List<NaturalAttack> Attacks = new List<NaturalAttack>();
        public List<Ability> NaturalAbilities = new List<Ability>();
        public List<Ability> OnDeath = new List<Ability>();
    }

    [Serializable]
    public class Equipment : BlueprintComponent
    {
        public List<Blueprint> Equipped = new List<Blueprint>();
    }

    [Serializable]
    public class Inventory : BlueprintComponent
    {
        public List<Blueprint> StoredItems = new List<Blueprint>();
    }

    [Serializable]
    public class Lootable : BlueprintComponent
    {
        public LootTable LootTable;
    }

    [Serializable]
    public class HasGold : BlueprintComponent
    {
        public string Amount;
    }

    [Serializable]
    public class Vendor : BlueprintComponent
    {
        public VendorCategory VendorCategory;
    }

    [Serializable]
    public class CreatureType : BlueprintComponent
    {
        public Deadspell.Core.CreatureType Type = Core.CreatureType.Humanoid;
    }

    [Serializable]
    public class Languages : BlueprintComponent
    {
        public List<Language> KnownLanguages = new List<Language>();
    }
}