using System;
using System.Collections.Generic;
using Deadspell.Data.Blueprints;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Deadspell.Data
{
    [Serializable]
    public struct RandomEntry<T>
    {
        public T Entry;
        public int Weight;
    }

    [Serializable]
    public class RandomTable<T>
    where T : class
    {
        [TableList]
        public List<RandomEntry<T>> Entries = new List<RandomEntry<T>>();
        [ReadOnly]
        public int TotalWeight = 0;

        public RandomTable<T> Add(T entry, int weight)
        {
            if (weight > 0)
            {
                TotalWeight += weight;
                Entries.Add(new RandomEntry<T>
                {
                    Entry = entry,
                    Weight = weight,
                });
            }

            return this;
        }

        [Button(ButtonSizes.Large)]
        public void RecalculateTotalWeight()
        {
            TotalWeight = 0;
            foreach (var entry in Entries)
            {
                TotalWeight += entry.Weight;
            }
        }

        public T Roll()
        {
            if (TotalWeight == 0)
            {
                return null;
            }

            var roll = Random.Range(0, TotalWeight);
            var index = 0;
            while (roll >= 0)
            {
                if (roll < Entries[index].Weight)
                {
                    return Entries[index].Entry;
                }

                roll -= Entries[index].Weight;
                index++;
            }

            return null;
        }
    }
    
    [CreateAssetMenu(menuName = "Deadspell/Spawn Table")]
    public class SpawnTable : SerializedScriptableObject
    {
        [Title("Items")]
        [HideLabel]
        public RandomTable<Blueprint> Items = new RandomTable<Blueprint>();
        
        [Title("Props")]
        [HideLabel]
        public RandomTable<Blueprint> Props = new RandomTable<Blueprint>();
        
        [Title("Characters")]
        [HideLabel]
        public RandomTable<Blueprint> Characters = new RandomTable<Blueprint>();

        public Blueprint Roll()
        {
            return Random.Range(0, 4) switch
            {
                0 => Items.Roll(),
                1 => Props.Roll(),
                2 => Characters.Roll(),
                _ => null,
            };
        }
    }
}