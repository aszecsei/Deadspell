using Deadspell.Data.Blueprints;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "Deadspell/Loot Table")]
    public class LootTable : SerializedScriptableObject
    {
        [HideLabel] public RandomTable<Blueprint> Drops = new RandomTable<Blueprint>();
    }
}