using System.Collections.Generic;
using Deadspell.Data.Blueprints;
using Deadspell.Map;
using UnityEngine;

namespace Deadspell.MapBuilders
{
    public class MapBuilder
    {
        public struct Data
        {
            // TODO:
            public List<(Vector2Int, Blueprint)> SpawnList;
            public MapData Map;
            public Vector2Int? StartingPosition;
            public List<RectInt> Rooms;
            public List<List<Vector2Int>> Corridors;
            public List<MapData> History;
            public int Width;
            public int Height;

            public void TakeSnapshot()
            {
                History.Add(Map.Clone());
            }
        }
        public IBaseMapBuilder Start;
        public List<IMetaMapBuilder> Builders;
        public Data BuildData;

        public static MapBuilder ExampleMapBuilder()
        {
            return new MapBuilder(60, 40, "Last Hearth")
                    .StartsWith(new SimpleMapBuilder())
                    .With(new RoomSorter(RoomSorter.Sort.Central))
                    .With(new RoomDrawer())
                    .With(new RoomCornerRounder())
                    .With(new StraightLineCorridors())
                    .With(new RoomExploder())
                    .With(new RoomBasedStartingPosition())
                    .With(new RoomBasedStairs())
                    .With(new RoomBasedSpawner())
                    ;
        }

        public MapBuilder(int width, int height, string name)
        {
            Start = null;
            Builders = new List<IMetaMapBuilder>();
            BuildData = new Data
            {
                Map = new MapData(name, width, height),
                StartingPosition = null,
                Rooms = null,
                Corridors = null,
                History = new List<MapData>(),
                Width = width,
                Height = height,
            };
        }

        public MapBuilder StartsWith(IBaseMapBuilder starter)
        {
            if (Start == null)
            {
                Start = starter;
            }
            else
            {
                Debug.LogError("You can only have one starting builder.");
            }

            return this;
        }

        public MapBuilder With(IMetaMapBuilder metabuilder)
        {
            Builders.Add(metabuilder);
            return this;
        }

        public void Reset()
        {
            string name = BuildData.Map.Name;
            int w = BuildData.Width;
            int h = BuildData.Height;
            
            BuildData = new Data
            {
                Map = new MapData(name, w, h),
                StartingPosition = null,
                Rooms = null,
                Corridors = null,
                History = new List<MapData>(),
                Width = w,
                Height = h,
            };
        }

        public Data Build()
        {
            if (Start == null)
            {
                Debug.LogError("Cannot run a map builder chain without a starting build system.");
                return BuildData;
            }
            
            Start.BuildMap(ref BuildData);

            foreach (var metabuilder in Builders)
            {
                metabuilder.BuildMap(ref BuildData);
            }

            return BuildData;
        }
    }
}