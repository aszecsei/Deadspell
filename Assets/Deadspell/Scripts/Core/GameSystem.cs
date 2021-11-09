using UnityEngine;

namespace Deadspell.Core
{
    public static class GameSystem
    {
        public static int AttributeBonus(int value)
        {
            return (value - 10) / 2;
        }

        public static int PlayerHealthPerLevel(Attribute vitality)
        {
            return 15 + vitality.Bonus;
        }
        
        public static int PlayerHealthAtLevel(Attribute vitality, int level)
        {
            return PlayerHealthPerLevel(vitality) * level;
        }

        public static int NpcHealth(Attribute vitality, int level)
        {
            int total = 1;
            for (int i = 0; i < level; i++)
            {
                total += Mathf.Max(1, 8 + vitality.Bonus);
            }

            return total;
        }

        public static int ManaPerLevel(Attribute wits)
        {
            return Mathf.Max(1, 4 + wits.Bonus);
        }

        public static int ManaAtLevel(Attribute wits, int level)
        {
            return ManaPerLevel(wits) * level;
        }

        public static int SkillBonus(Skill skill, Skills skills)
        {
            if (skills.SkillValues.ContainsKey(skill))
            {
                return skills.SkillValues[skill];
            }
            else
            {
                return -4;
            }
        }

        public static int ExpToLevel(int level)
        {
            return level * 1000;
        }

        public static int ExpGainByLevel(int level)
        {
            return level * 100;
        }
    }
}