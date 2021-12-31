using System;
using System.Collections.Generic;
using UnityEngine;


namespace Deadspell.Map
{
    public static class FieldOfView
    {
        private struct Tile
        {
            public int Depth;
            public int Column;

            public Tile(int depth, int column)
            {
                Depth = depth;
                Column = column;
            }
        }

        private enum Cardinal
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3,
        }

        private struct Quadrant
        {
            private Cardinal _cardinal;
            private Vector2Int _origin;

            public Cardinal Cardinal => _cardinal;
            public Vector2Int Origin => _origin;

            public Quadrant(Cardinal cardinal, Vector2Int origin)
            {
                _cardinal = cardinal;
                _origin = origin;
            }

            public Vector2Int Transform(Tile tile)
            {
                int depth = tile.Depth;
                int col = tile.Column;
                int ox = _origin.x;
                int oy = _origin.y;

                return _cardinal switch
                {
                    Cardinal.North => new Vector2Int(ox + col, oy - depth),
                    Cardinal.South => new Vector2Int(ox + col, oy + depth),
                    Cardinal.East => new Vector2Int(ox + depth, oy + col),
                    Cardinal.West => new Vector2Int(ox - depth, oy + col),
                    _ => throw new ArgumentOutOfRangeException(nameof(_cardinal)),
                };
            }
        }
        
        private struct Scanline
        {
            public int Depth;
            public Rational StartSlope;
            public Rational EndSlope;

            public Scanline(int depth, Rational startSlope, Rational endSlope)
            {
                Depth = depth;
                StartSlope = startSlope;
                EndSlope = endSlope;
            }
            
            public IEnumerable<Tile> Tiles()
            {
                var startCol = RoundTiesUp(Depth * StartSlope);
                var endCol = RoundTiesDown(Depth * EndSlope);
                var depth = Depth;
                for (var col = startCol; col <= endCol; col++)
                {
                    yield return new Tile(depth, (int)col);
                }
            }

            public Scanline Next()
            {
                return new Scanline(Depth + 1, StartSlope, EndSlope);
            }
        }

        private static Rational Slope(Tile tile)
        {
            return new Rational(2 * tile.Column - 1, 2 * tile.Depth);
        }

        private static bool IsSymmetric(Scanline scanline, Tile tile)
        {
            var col = new Rational(tile.Column);
            var depth = new Rational(scanline.Depth);

            return (col >= (depth * scanline.StartSlope))
                   && (col <= (depth * scanline.EndSlope));
        }

        private static long RoundTiesUp(Rational f)
        {
            return (f + new Rational(1, 2)).FloorToInt();
        }

        private static long RoundTiesDown(Rational f)
        {
            return (f - new Rational(1, 2)).CeilToInt();
        }

        private static void ComputeFov(Vector2Int origin, int range, Func<Vector2Int, bool> isOpaque, Action<Vector2Int> markVisible)
        {
            markVisible(origin);

            var radiusPlusHalf = new Rational(range) + new Rational(1, 2);
            var radiusPlusHalfSq = radiusPlusHalf * radiusPlusHalf;
            var radiusSq = range * range;
            
            foreach (Cardinal cardinal in Enum.GetValues(typeof(Cardinal)))
            {
                var quadrant = new Quadrant(cardinal, origin);

                void Reveal(Tile tile)
                {
                    var xy = quadrant.Transform(tile);
                    markVisible(xy);
                }

                bool IsOpaque(Tile tile)
                {
                    var xy = quadrant.Transform(tile);
                    bool result = isOpaque(xy);
                    return result;
                }

                void Scan(Scanline scanline)
                {
                    Stack<Scanline> stack = new Stack<Scanline>();
                    stack.Push(scanline);

                    while (stack.Count > 0)
                    {
                        var line = stack.Pop();
                        if (line.Depth * line.Depth > radiusSq)
                        {
                            continue;
                        }

                        Tile? prevTile = null;
                        foreach (var tile in line.Tiles())
                        {
                            var tp = quadrant.Transform(tile);
                            var dx = tp.x - quadrant.Origin.x;
                            var dy = tp.y - quadrant.Origin.y;
                            if (true || new Rational(dx * dx + dy * dy) <= radiusPlusHalfSq)
                            {
                                if (IsOpaque(tile) || IsSymmetric(line, tile))
                                {
                                    Reveal(tile);
                                }

                                if (prevTile.HasValue)
                                {
                                    if (IsOpaque(prevTile.Value) && !IsOpaque(tile))
                                    {
                                        line.StartSlope = Slope(tile);
                                    }

                                    if (!IsOpaque(prevTile.Value) && IsOpaque(tile))
                                    {
                                        var nextLine = line.Next();
                                        nextLine.EndSlope = Slope(tile);
                                        stack.Push(nextLine);
                                    }
                                }

                                prevTile = tile;
                            }
                        }

                        if (prevTile.HasValue)
                        {
                            if (!IsOpaque(prevTile.Value))
                            {
                                stack.Push(line.Next());
                            }
                        }
                    }
                }
                
                var first = new Scanline(1, -1, 1);
                Scan(first);
            }
        }

        // https://www.albertford.com/shadowcasting/#symmetry
        public static HashSet<Vector2Int> Calculate(int x, int y, int range, MapData map)
        {
            HashSet<Vector2Int> visible = new HashSet<Vector2Int>();
            
            ComputeFov(new Vector2Int(x, y),
                range,
                (pos) => map.Spatial.IsOpaque(pos.x, pos.y),
                (pos) => visible.Add(pos)
            );

            return visible;
        }
        public static void Calculate(int x, int y, int range, MapData map, HashSet<Vector2Int> visible)
        {
            visible.Clear();
            
            ComputeFov(new Vector2Int(x, y),
                range,
                (pos) => map.Spatial.IsOpaque(pos.x, pos.y),
                (pos) => visible.Add(pos)
            );
        }
    }
}