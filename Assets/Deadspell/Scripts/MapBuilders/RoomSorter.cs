using System;
using UnityEngine;

namespace Deadspell.MapBuilders
{
    public class RoomSorter : IMetaMapBuilder
    {
        public enum Sort
        {
            Leftmost,
            Rightmost,
            Topmost,
            Bottommost,
            Central,
        }

        public Sort SortBy;

        public RoomSorter(Sort sort)
        {
            SortBy = sort;
        }

        public void BuildMap(ref MapBuilder.Data data)
        {
            Sorter(ref data);
        }

        private void Sorter(ref MapBuilder.Data data)
        {
            switch (SortBy)
            {
                case Sort.Leftmost:
                    data.Rooms.Sort((a, b) => a.xMin - b.xMin);
                    break;
                case Sort.Rightmost:
                    data.Rooms.Sort((a, b) => b.xMax - a.xMax);
                    break;
                case Sort.Topmost:
                    data.Rooms.Sort((a, b) => a.yMin - b.yMin);
                    break;
                case Sort.Bottommost:
                    data.Rooms.Sort((a, b) => b.yMax - a.yMax);
                    break;
                case Sort.Central:
                    var mapCenter = new Vector2Int(data.Width / 2, data.Height / 2);
                    data.Rooms.Sort((a, b) =>
                    {
                        var aC = Vector2Int.RoundToInt(a.center);
                        var bC = Vector2Int.RoundToInt(b.center);
                        var dA = Vector2Int.Distance(aC, mapCenter);
                        var dB = Vector2Int.Distance(bC, mapCenter);
                        return Mathf.RoundToInt(dA - dB);
                    });
                    break;
                default:
                    Debug.LogError($"Unexpected enum variant {SortBy}.");
                    break;
            }
        }
    }
}