using System;
using System.Collections.Generic;
using Deadspell.Data.Recipes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "Deadspell/Loot Table")]
    public class LootTable : SerializedScriptableObject
    {
        [Serializable]
        public class LootTableEntry
        {
            public ItemRecipe Drop;
            public int Weight;
        }

        [TableList(AlwaysExpanded = true)]
        [HideLabel]
        public List<LootTableEntry> Drops = new List<LootTableEntry>();
    }
}