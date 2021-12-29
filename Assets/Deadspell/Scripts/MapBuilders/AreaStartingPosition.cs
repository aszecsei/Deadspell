using System;
using System.Collections.Generic;
using Deadspell.Map;
using UnityEngine;

namespace Deadspell.MapBuilders
{
    public class AreaStartingPosition : IMetaMapBuilder
    {
        public enum XStart
        {
            Left,
            Center,
            Right,
        }

        public enum YStart
        {
            Top,
            Center,
            Bottom,
        }

        private XStart _x;
        private YStart _y;

        public AreaStartingPosition(XStart x, YStart y)
        {
            this._x = x;
            this._y = y;
        }
        
        public void BuildMap(ref MapBuilder.Data data)
        {
            Build(ref data);
        }

        private void Build(ref MapBuilder.Data data)
        {
            var seedX = _x switch
            {
                XStart.Left => 1,
                XStart.Center => data.Map.Width / 2,
                XStart.Right => data.Map.Width - 2,
                _ => throw new Exception($"Unexpected enum {_x}"),
            };
            var seedY = _y switch
            {
                YStart.Top => 1,
                YStart.Center => data.Map.Height / 2,
                YStart.Bottom => data.Map.Height - 2,
                _ => throw new Exception($"Unexpected enum {_y}"),
            };

            var availableFloors = new List<(Vector2Int, float)>();
            for (int y = 0; y < data.Map.Height; y++)
            {
                for (int x = 0; x < data.Map.Width; x++)
                {
                    if (data.Map[x, y].Type.Walkable())
                    {
                        var seed = new Vector2Int(seedX, seedY);
                        var pt = new Vector2Int(x, y);
                        availableFloors.Add((pt, Vector2Int.Distance(pt, seed)));
                    }
                }
            }

            if (availableFloors.Count == 0)
            {
                Debug.LogError("No valid floors to start on");
                return;
            }
            
            availableFloors.Sort((a, b) => a.Item2.CompareTo(b.Item2));
            data.StartingPosition = availableFloors[0].Item1;
        }
    }
}