using System.Collections.Generic;
using Deadspell.Data.Recipes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "Deadspell/Spawn Table")]
    public class SpawnTable : SerializedScriptableObject
    {
        public class SpawnTableElement<T> where T : Recipe
        {
            public T Entry;
            public int Weight;
        }

        [TableList]
        public List<SpawnTableElement<ItemRecipe>> Items = new List<SpawnTableElement<ItemRecipe>>();
    }
}