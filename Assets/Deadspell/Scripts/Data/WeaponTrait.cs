using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "Deadspell/Weapons/Effect")]
    public class WeaponTrait : SerializedScriptableObject
    {
        public string Name;
        public List<Effect> Effects = new List<Effect>();
    }
}