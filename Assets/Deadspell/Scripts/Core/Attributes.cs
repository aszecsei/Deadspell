using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Core
{
    [Serializable]
    public class Attribute
    {
        public int Base = 10;
        public int Modifiers = 0;
        public int Bonus => GameSystem.AttributeBonus(Base + Modifiers);
    }

    public enum AttributeType
    {
        Might,
        Agility,
        Vitality,
        Intelligence,
        Wits,
        Resolve,
        Faith,
        Insight,
        Conviction,
        Presence,
        Manipulation,
        Composure,
        Luck,
    }
    
    [Serializable]
    public class Attributes
    {
        [Serializable]
        public class Modifier
        {
            public int Might;
            public int Agility;
            public int Vitality;

            public int Intelligence;
            public int Wits;
            public int Resolve;

            public int Faith;
            public int Insight;
            public int Conviction;

            public int Presence;
            public int Manipulation;
            public int Composure;

            public int Luck;
        }
        
        public Attribute Might = new Attribute();
        public Attribute Agility = new Attribute();
        public Attribute Vitality = new Attribute();
        
        public Attribute Intelligence = new Attribute();
        public Attribute Wits = new Attribute();
        public Attribute Resolve = new Attribute();

        public Attribute Faith = new Attribute();
        public Attribute Insight = new Attribute();
        public Attribute Conviction = new Attribute();

        public Attribute Presence = new Attribute();
        public Attribute Manipulation = new Attribute();
        public Attribute Composure = new Attribute();
        
        public Attribute Luck = new Attribute();

        public void ApplyModifier(Modifier modifier)
        {
            Might.Modifiers += modifier.Might;
            Agility.Modifiers += modifier.Agility;
            Vitality.Modifiers += modifier.Vitality;
            
            Intelligence.Modifiers += modifier.Intelligence;
            Wits.Modifiers += modifier.Wits;
            Resolve.Modifiers += modifier.Resolve;
            
            Faith.Modifiers += modifier.Faith;
            Insight.Modifiers += modifier.Insight;
            Conviction.Modifiers += modifier.Conviction;
            
            Presence.Modifiers += modifier.Presence;
            Manipulation.Modifiers += modifier.Manipulation;
            Composure.Modifiers += modifier.Composure;
            
            Luck.Modifiers += modifier.Luck;
        }

        public void RemoveModifier(Modifier modifier)
        {
            Might.Modifiers -= modifier.Might;
            Agility.Modifiers -= modifier.Agility;
            Vitality.Modifiers -= modifier.Vitality;
            
            Intelligence.Modifiers -= modifier.Intelligence;
            Wits.Modifiers -= modifier.Wits;
            Resolve.Modifiers -= modifier.Resolve;
            
            Faith.Modifiers -= modifier.Faith;
            Insight.Modifiers -= modifier.Insight;
            Conviction.Modifiers -= modifier.Conviction;
            
            Presence.Modifiers -= modifier.Presence;
            Manipulation.Modifiers -= modifier.Manipulation;
            Composure.Modifiers -= modifier.Composure;
            
            Luck.Modifiers -= modifier.Luck;
        }
    }
}