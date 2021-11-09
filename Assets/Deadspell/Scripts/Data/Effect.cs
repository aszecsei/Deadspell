using Deadspell.Core;
using Deadspell.ValueReferences;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    public abstract class Effect
    {
    }

    public class HealingEffect : Effect
    {
        public string Amount;
    }

    public class HealingOverTimeEffect : Effect
    {
        public string Amount;
        public int Duration;
    }

    public class ManaGainEffect : Effect
    {
        public string Amount;
    }

    public class DamageEffect : Effect
    {
        public Damage Damage;
    }

    public class DamageOverTimeEffect : Effect
    {
        public Damage Damage;
        public int Duration;
    }

    public class StatusEffect : Effect
    {
        public StatusEffects Effect;
        public int Duration;
    }

    public class AoEEffect : Effect
    {
        public float Radius;
    }

    public class ParticleEffect : Effect
    {
        public Sprite Particle;
        public ColorReference Color;
        public float Duration = 0.4f;
    }
    
    public class ParticleLineEffect : Effect
    {
        public Sprite Particle;
        public ColorReference Color;
        public float Duration = 0.4f;
    }
    
    public class TeachSpellEffect : Effect
    {
        public Spell Spell;
    }

    public class CastSpellEffect : Effect
    {
        public Spell Spell;
    }

    public class MagicMappingEffect : Effect
    {
    }

    public class TownPortalEffect : Effect
    {
    }

    public class FoodEffect : Effect
    {
    }

    public class RemoveCurseEffect : Effect
    {
    }

    public class IdentifyEffect : Effect
    {
    }
}