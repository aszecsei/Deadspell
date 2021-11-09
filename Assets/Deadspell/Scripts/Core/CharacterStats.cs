using System;
using Sirenix.OdinInspector;

namespace Deadspell.Core
{
    public class CharacterStats : SerializedMonoBehaviour
    {
        [Serializable]
        public class Pool
        {
            public int Current;
            public int Max;

            public Pool(int max)
            {
                Current = max;
                Max = max;
            }
        }

        public Pool Health;
        public Pool Mana;
        public int Experience;
        public int Level;
        public float TotalWeight;
        public float TotalInitiativePenalty;
        public float Gold;
        public bool GodMode;
    }
}