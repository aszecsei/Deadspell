using Deadspell.Map;
using UnityEngine;

namespace Deadspell.MapBuilders
{
    public class RoomExploder : IMetaMapBuilder
    {
        public void BuildMap(ref MapBuilder.Data data)
        {
            Build(ref data);
        }

        private void Build(ref MapBuilder.Data data)
        {
            if (data.Rooms == null)
            {
                Debug.LogError("Nearest corridors require a builder with room structures.");
                return;
            }

            foreach (var room in data.Rooms)
            {
                var start = Vector2Int.RoundToInt(room.center);
                var numDiggers = Random.Range(1, 21) - 5;
                if (numDiggers > 0)
                {
                    for (int i = 0; i < numDiggers; i++)
                    {
                        var drunk = start;
                        var drunkLife = 20;
                        var didSomething = false;

                        while (drunkLife > 0)
                        {
                            if (!data.Map.IsInBounds(drunk.x, drunk.y))
                            {
                                break;
                            }
                            
                            if (data.Map[drunk].Type == TileType.Wall)
                            {
                                didSomething = true;
                            }
                            Utils.Paint(data.Map, Symmetry.None, 1, drunk);
                            data.Map.SetTileType(drunk.x, drunk.y, TileType.DownStairs);

                            var staggerDirection = Random.Range(1, 5);
                            switch (staggerDirection)
                            {
                                case 1:
                                {
                                    if (drunk.x >= 2)
                                    {
                                        drunk.x -= 1;
                                    }
                                } break;
                                case 2:
                                {
                                    if (drunk.x < data.Map.Width - 2)
                                    {
                                        drunk.x += 1;
                                    }
                                } break;
                                case 3:
                                {
                                    if (drunk.y >= 2)
                                    {
                                        drunk.y -= 1;
                                    }
                                } break;
                                case 4:
                                {
                                    if (drunk.y < data.Map.Width - 2)
                                    {
                                        drunk.y += 1;
                                    }
                                } break;
                            }

                            drunkLife -= 1;
                        }

                        if (didSomething)
                        {
                            data.TakeSnapshot();
                        }

                        for (int y = 0; y < data.Map.Height; y++)
                        {
                            for (int x = 0; x < data.Map.Width; x++)
                            {
                                if (data.Map[x, y].Type == TileType.DownStairs)
                                {
                                    data.Map.SetTileType(x, y, TileType.Floor);
                                }
                            }
                        }
                    }
                }
            }
            data.TakeSnapshot();
        }
    }
}