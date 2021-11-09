using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data.Recipes
{
    [CreateAssetMenu(menuName = "Deadspell/Prop")]
    public class PropRecipe : Recipe
    {
        public string Name;
        public Renderable Renderable = new Renderable();
        public bool Hidden = false;
        public bool Door = false;
        public bool BlocksTile = false;
        public bool BlocksVisibility = false;
        public LightData Light = null;
        public List<Effect> EntityTrigger = new List<Effect>();
        [ShowIf("ShowSingleActivation")]
        public bool SingleActivation = false;

        private bool ShowSingleActivation()
        {
            return EntityTrigger != null;
        }
    }
}