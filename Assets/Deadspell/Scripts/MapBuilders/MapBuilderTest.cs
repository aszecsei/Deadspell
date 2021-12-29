using System.Collections;
using Deadspell.Core;
using Deadspell.Map;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.EditorCoroutines.Editor;

namespace Deadspell.MapBuilders
{
    public class MapBuilderTest : SerializedMonoBehaviour
    {
        public Tilemap Tilemap;
        public MapThemeManager MapThemeManager;
        
        [ShowInInspector]
        [ReadOnly]
        private MapBuilder _builder;
        private EditorCoroutine _mapBuilding = null;

        [Button(ButtonSizes.Large)]
        public void DemoMapBuilder()
        {
            _builder = MapBuilder.ExampleMapBuilder();
            var data = _builder.Build();
            if (_mapBuilding == null)
            {
                _mapBuilding = EditorCoroutineUtility.StartCoroutine(ShowMapBuildingProcess(data), this);
            }
        }

        [ShowIf("_mapBuilding", null)]
        [Button(ButtonSizes.Large)]
        public void CancelDemo()
        {
            if (_mapBuilding != null)
            {
                EditorCoroutineUtility.StopCoroutine(_mapBuilding);
                _mapBuilding = null;
            }
        }

        private void DrawMap(MapData mapData)
        {
            // Clear tilemap
            Tilemap.ClearAllTiles();
            
            // Draw map data
            for (var y = 0; y < mapData.Height; y++)
            {
                for (var x = 0; x < mapData.Width; x++)
                {
                    var tile = MapThemeManager.ForestTheme.GetTile(mapData[x, y].Type);
                    Tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }

        private IEnumerator ShowMapBuildingProcess(MapBuilder.Data data)
        {
            foreach (var mapHistory in data.History)
            {
                DrawMap(mapHistory);
                yield return new EditorWaitForSeconds(0.2f);
            }

            _mapBuilding = null;
        }
    }
}