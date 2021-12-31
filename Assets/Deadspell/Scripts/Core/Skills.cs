using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Core
{
    [Serializable]
    [Flags]
    public enum Skill
    {
        Alchemy = 1 << 1,
        AnimalHandling = 1 << 2,
        Arcana = 1 << 3,
        Brewing = 1 << 4,
        Calligraphy = 1 << 5,
        Cartography = 1 << 6,
        Cooking = 1 << 7,
        Deception = 1 << 8,
        Disguise = 1 << 9,
        Forgery = 1 << 10,
        HeavyArmor = 1 << 11,
        Herbalism = 1 << 12,
        History = 1 << 13,
        Intimidation = 1 << 14,
        LightArmor = 1 << 15,
        Lockpicking = 1 << 16,
        MartialWeapons = 1 << 17,
        Medicine = 1 << 18,
        MediumArmor = 1 << 19,
        Nature = 1 << 20,
        Perception = 1 << 21,
        Performance = 1 << 22,
        Persuasion = 1 << 23,
        Poison = 1 << 24,
        Religion = 1 << 25,
        Shields = 1 << 26,
        SimpleWeapons = 1 << 27,
        Smithing = 1 << 28,
        Stealth = 1 << 29,
    }
    
    public class Skills
    {
        public Dictionary<Skill, int> SkillValues;

        public Skills()
        {
            SkillValues = new Dictionary<Skill, int>
            {
                { Skill.SimpleWeapons, 1 },
                { Skill.LightArmor, 1 },
                { Skill.Arcana, 1 },
                { Skill.Perception, 1 },
            };
        }

        public int Bonus(Skill skill)
        {
            return GameSystem.SkillBonus(skill, this);
        }
    }
}