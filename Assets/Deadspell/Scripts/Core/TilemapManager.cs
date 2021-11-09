using UnityEngine;
using UnityEngine.Tilemaps;

namespace Deadspell.Core
{
    public class TilemapManager : MonoBehaviour
    {
        public static TilemapManager Instance;
        public Tilemap Tilemap;
        public Vector3 Offset = new Vector3(0.5f, 0.5f, 0f);

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            Tilemap.ClearAllTiles();
            Tilemap.tileAnchor = Offset;
        }

        void Update()
        {
        }
    }
}