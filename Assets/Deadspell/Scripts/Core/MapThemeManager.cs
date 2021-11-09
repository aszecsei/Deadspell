using Deadspell.Data;
using UnityEngine;

namespace Deadspell.Core
{
    public class MapThemeManager : MonoBehaviour
    {
        public static MapThemeManager Instance;

        public MapTheme ForestTheme;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}