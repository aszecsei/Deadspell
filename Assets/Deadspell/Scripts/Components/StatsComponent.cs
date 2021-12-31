﻿using System;
using Entitas;

namespace Deadspell.Components
{
    [Game]
    public class StatsComponent : IComponent
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
        public bool GodMode;
    }
    
    [Game]
    public class StatsDirtyComponent : IComponent
    {
    }
}