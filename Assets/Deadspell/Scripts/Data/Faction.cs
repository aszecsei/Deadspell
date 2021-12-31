using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "Deadspell/Faction")]
    public class Faction : SerializedScriptableObject
    {
        [LabelWidth(150)]
        public string DisplayName;
        [LabelWidth(150)]
        public bool Visible = true;
        [LabelWidth(150)]
        public int DefaultOpinion = 0;
        [Title("Description", bold: false)]
        [HideLabel]
        [MultiLineProperty(10)]
        public string Description;
        public Dictionary<Faction, int> Opinions = new Dictionary<Faction, int>();

        public int OpinionOf(Faction other)
        {
            if (Opinions.TryGetValue(other, out var opinion))
            {
                return opinion;
            }

            return DefaultOpinion;
        }
    }
}