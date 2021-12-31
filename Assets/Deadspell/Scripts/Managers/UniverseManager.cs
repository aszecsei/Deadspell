using Deadspell.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Managers
{
    public class UniverseManager : MonoBehaviour
    {
        private static UniverseManager _instance;

        public static UniverseManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UniverseManager>();
                }

                return _instance;
            }
        }

        [BoxGroup("Player Data")]
        [InlineEditor()]
        public Faction PlayerFaction;

        [BoxGroup("Player Data")]
        [InlineEditor()]
        public Tile PlayerTile;
    }
}