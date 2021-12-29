using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deadspell.Map
{
    [Serializable]
    public class MapData
    {
        public class TileData
        {
            public TileType Type;
            public bool Revealed;
            public bool Visible;

            public TileData Clone()
            {
                return new TileData
                {
                    Type = Type,
                    Revealed = Revealed,
                    Visible = Visible,
                };
            }
        }

        public string Name;
        public TileData[,] Tiles;
        public SpatialMap Spatial;
        public int Width;
        public int Height;

        private MapData()
        {
            Name = "Temporary";
            Tiles = new TileData[0, 0];
            Width = 0;
            Height = 0;
            Spatial = new SpatialMap();
        }

        public MapData(string name, int width, int height)
        {
            Name = name;
            Tiles = new TileData[width, height];
            Width = width;
            Height = height;
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Tiles[x, y] = new TileData
                    {
                        Type = TileType.Wall,
                        Revealed = false,
                        Visible = false,
                    };
                }
            }

            Spatial = new SpatialMap();
            Spatial.SetSize(width, height);
        }

        public TileData this[int x, int y]
        {
            get => Tiles[x, y];
            set => Tiles[x, y] = value;
        }
        public TileData this[Vector2Int pos]
        {
            get => Tiles[pos.x, pos.y];
            set => Tiles[pos.x, pos.y] = value;
        }

        public void SetTileType(int x, int y, TileType tt)
        {
            Tiles[x, y].Type = tt;
        }

        public bool IsTransparent(int x, int y)
        {
            return !Spatial.IsOpaque(x, y);
        }

        public bool IsWalkable(int x, int y)
        {
            return !Spatial.IsBlocked(x, y);
        }

        public bool IsInBounds(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return false;
            }

            return true;
        }

        private bool IsPositionOpen(int x, int y)
        {
            if (!IsInBounds(x, y))
            {
                return false;
            }

            return !Spatial.IsBlocked(x, y);
        }

        public List<Tuple<int, int, float>> GetAvailableExits(int x, int y)
        {
            TileType tt = this[x, y].Type;
            List<Tuple<int, int, float>> exits = new List<Tuple<int, int, float>>();
            
            if (IsPositionOpen(x - 1, y))
            {
                exits.Add(new Tuple<int, int, float>(x - 1, y, tt.Cost()));
            }
            if (IsPositionOpen(x + 1, y))
            {
                exits.Add(new Tuple<int, int, float>(x + 1, y, tt.Cost()));
            }
            if (IsPositionOpen(x, y - 1))
            {
                exits.Add(new Tuple<int, int, float>(x, y - 1, tt.Cost()));
            }
            if (IsPositionOpen(x, y + 1))
            {
                exits.Add(new Tuple<int, int, float>(x, y + 1, tt.Cost()));
            }
            
            const float DIAGONAL_COST = 1.5f;
            if (IsPositionOpen(x - 1, y - 1))
            {
                exits.Add(new Tuple<int, int, float>(x - 1, y - 1, tt.Cost() * DIAGONAL_COST));
            }
            if (IsPositionOpen(x + 1, y - 1))
            {
                exits.Add(new Tuple<int, int, float>(x + 1, y - 1, tt.Cost() * DIAGONAL_COST));
            }
            if (IsPositionOpen(x - 1, y + 1))
            {
                exits.Add(new Tuple<int, int, float>(x - 1, y + 1, tt.Cost() * DIAGONAL_COST));
            }
            if (IsPositionOpen(x + 1, y + 1))
            {
                exits.Add(new Tuple<int, int, float>(x + 1, y + 1, tt.Cost() * DIAGONAL_COST));
            }
            
            return exits;
        }

        public float GetPathingDistance(int x1, int y1, int x2, int y2)
        {
            var dx = x2 - x1;
            var dy = y2 - y1;
            return Mathf.Sqrt(dx * dx + dy * dy);
        }

        public void PopulateBlocked()
        {
            Spatial.PopulateBlockedFromMap(this);
        }

        public void ClearBlocked()
        {
            Spatial.Clear();
        }

        public MapData Clone()
        {
            var tilesClone = (TileData[,])Tiles.Clone();
            for (var x = 0; x < tilesClone.GetLength(0); x++)
            {
                for (var y = 0; y < tilesClone.GetLength(1); y++)
                {
                    tilesClone[x, y] = tilesClone[x, y].Clone();
                }
            }
            return new MapData
            {
                Width = Width,
                Height = Height,
                Name = Name,
                Spatial = (SpatialMap)Spatial.Clone(),
                Tiles = tilesClone,
            };
        }
    }
}