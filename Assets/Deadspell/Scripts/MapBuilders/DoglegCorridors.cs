using System.Collections.Generic;
using UnityEngine;

namespace Deadspell.MapBuilders
{
    public class DoglegCorridors : IMetaMapBuilder
    {
        public void BuildMap(ref MapBuilder.Data data)
        {
            Corridors(ref data);
        }

        private void Corridors(ref MapBuilder.Data data)
        {
            if (data.Rooms == null)
            {
                Debug.LogError("Dogleg corridors require a builder with room structures.");
                return;
            }

            var corridors = new List<List<Vector2Int>>();
            for (int i = 0; i < data.Rooms.Count; i++)
            {
                if (i > 0)
                {
                    var newC = Vector2Int.RoundToInt(data.Rooms[i].center);
                    var prevC = Vector2Int.RoundToInt(data.Rooms[i - 1].center);

                    if (Random.Range(0, 2) == 0)
                    {
                        var c1 = Utils.ApplyHorizontalTunnel(data.Map, prevC.x, newC.x, prevC.y);
                        var c2 = Utils.ApplyVerticalTunnel(data.Map, prevC.y, newC.y, newC.x);
                        c1.AddRange(c2);
                        corridors.Add(c1);
                    }
                    else
                    {
                        var c1 = Utils.ApplyVerticalTunnel(data.Map, prevC.y, newC.y, prevC.x);
                        var c2 = Utils.ApplyHorizontalTunnel(data.Map, prevC.x, newC.x, newC.y);
                        c1.AddRange(c2);
                        corridors.Add(c1);
                    }
                    
                    data.TakeSnapshot();
                }
            }

            data.Corridors = corridors;
        }
    }
}