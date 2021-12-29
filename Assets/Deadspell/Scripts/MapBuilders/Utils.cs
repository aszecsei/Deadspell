using System;
using System.Collections.Generic;
using Deadspell.Map;
using UnityEngine;

namespace Deadspell.MapBuilders
{
    public enum Symmetry
    {
        None,
        Horizontal,
        Vertical,
        Both,
    }
    
    public static class Utils
    {
        public static List<Vector2Int> ApplyHorizontalTunnel(MapData map, int x1, int x2, int y)
        {
            var corridor = new List<Vector2Int>();

            for (int x = Mathf.Min(x1, x2); x <= Mathf.Max(x1, x2); x++)
            {
                if (map[x, y].Type != TileType.Floor)
                {
                    map.SetTileType(x, y, TileType.Floor);
                    corridor.Add(new Vector2Int(x, y));
                }
            }
            
            return corridor;
        }
        
        public static List<Vector2Int> ApplyVerticalTunnel(MapData map, int y1, int y2, int x)
        {
            var corridor = new List<Vector2Int>();

            for (int y = Mathf.Min(y1, y2); y <= Mathf.Max(y1, y2); y++)
            {
                if (map[x, y].Type != TileType.Floor)
                {
                    map.SetTileType(x, y, TileType.Floor);
                    corridor.Add(new Vector2Int(x, y));
                }
            }
            
            return corridor;
        }

        public static List<Vector2Int> DrawCorridor(MapData map, Vector2Int from, Vector2Int to)
        {
            var corridor = new List<Vector2Int>();
            var pt = from;

            while (pt.x != to.x || pt.y != to.y)
            {
                if (pt.x < to.x)
                {
                    pt.x++;
                }
                else if (pt.x > to.x)
                {
                    pt.x--;
                }
                else if (pt.y < to.y)
                {
                    pt.y++;
                }
                else if (pt.y > to.y)
                {
                    pt.y--;
                }

                if (map[pt.x, pt.y].Type != TileType.Floor)
                {
                    map.SetTileType(pt.x, pt.y, TileType.Floor);
                    corridor.Add(pt);
                }
            }

            return corridor;
        }

        public static void Paint(MapData map, Symmetry mode, int brushSize, Vector2Int at)
        {
            switch (mode)
            {
                case Symmetry.None:
                {
                    ApplyPaint(map, brushSize, at.x, at.y);
                } break;
                case Symmetry.Horizontal:
                {
                    var centerX = map.Width / 2;
                    if (at.x == centerX)
                    {
                        ApplyPaint(map, brushSize, at.x, at.y);
                    }
                    else
                    {
                        var distX = Mathf.Abs(centerX - at.x);
                        ApplyPaint(map, brushSize, centerX + distX, at.y);
                        ApplyPaint(map, brushSize, centerX - distX, at.y);
                    }
                } break;
                case Symmetry.Vertical:
                {
                    var centerY = map.Height / 2;
                    if (at.y == centerY)
                    {
                        ApplyPaint(map, brushSize, at.x, at.y);
                    }
                    else
                    {
                        var distY = Mathf.Abs(centerY - at.y);
                        ApplyPaint(map, brushSize, at.x, centerY - distY);
                        ApplyPaint(map, brushSize, at.x, centerY + distY);
                    }
                } break;
                case Symmetry.Both:
                {
                    var centerX = map.Width / 2;
                    var centerY = map.Height / 2;
                    if (at.x == centerX && at.y == centerY)
                    {
                        ApplyPaint(map, brushSize, at.x, at.y);
                    }
                    else
                    {
                        var distX = Mathf.Abs(centerX - at.x);
                        var distY = Mathf.Abs(centerY - at.y);
                        ApplyPaint(map, brushSize, centerX + distX, centerY + distY);
                        ApplyPaint(map, brushSize, centerX + distX, centerY - distY);
                        ApplyPaint(map, brushSize, centerX - distX, centerY + distY);
                        ApplyPaint(map, brushSize, centerX - distX, centerY - distY);
                    }
                } break;
                default:
                    throw new Exception($"Unexpected symmetry mode {mode}");
            }
        }

        private static void ApplyPaint(MapData map, int brushSize, int x, int y)
        {
            if (brushSize == 1)
            {
                map.SetTileType(x, y, TileType.Floor);
            }
            else
            {
                int halfBrushSize = brushSize / 2;
                for (int brushY = y - halfBrushSize; brushY < y + halfBrushSize; brushY++)
                {
                    for (int brushX = x - halfBrushSize; brushX < x + halfBrushSize; brushX++)
                    {
                        if (map.IsInBounds(brushX, brushY))
                        {
                            map.SetTileType(brushX, brushY, TileType.Floor);
                        }
                    }
                }
            }
        }

        public static List<Vector2Int> Bresenham(Vector2Int from, Vector2Int to)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            int dx = Mathf.Abs(to.x - from.x);
            int sx = from.x < to.x ? 1 : -1;
            int dy = -Mathf.Abs(to.y - from.y);
            int sy = from.y < to.y ? 1 : -1;
            int err = dx + dy;

            Vector2Int curr = from;
            for (;;)
            {
                result.Add(curr);
                if (curr == to)
                {
                    break;
                }

                int e2 = 2 * err;
                if (e2 >= dy) /* e_xy + e_x > 0 */
                {
                    err += dy;
                    curr += Vector2Int.right * sx;
                }

                if (e2 <= dx) /* e_xy + e_y < 0 */
                {
                    err += dx;
                    curr += Vector2Int.up * sy;
                }
            }

            return result;
        }

        public static Vector2Int RandomPoint(this RectInt room)
        {
            return new Vector2Int(
                UnityEngine.Random.Range(room.xMin, room.xMax + 1),
                UnityEngine.Random.Range(room.yMin, room.yMax + 1)
            );
        }
    }
}