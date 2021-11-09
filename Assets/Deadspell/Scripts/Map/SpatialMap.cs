using System.Collections.Generic;
using UnityEngine;

namespace Deadspell.Map
{
    public class SpatialMap
    {
        public struct CellContent
        {
            public GameEntity Entity;
            public bool BlocksTile;
            public bool Opaque;
        }
        
        public class Cell
        {
            public bool BlockedByMap = false;
            public bool BlockedByEntity = false;
            public bool OpaqueByMap = false;
            public bool OpaqueByEntity = false;
            public List<CellContent> Content = new List<CellContent>();

            public bool Blocked
            {
                get => BlockedByEntity || BlockedByMap;
            }

            public bool Opaque
            {
                get => OpaqueByMap || OpaqueByEntity;
            }
        }

        public Cell[,] Cells;

        public IEnumerable<GameEntity> TileContent(int x, int y)
        {
            foreach (var c in Cells[x, y].Content)
            {
                yield return c.Entity;
            }
        }

        public void SetSize(int width, int height)
        {
            Cells = new Cell[width, height];
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                for (int x = 0; x < Cells.GetLength(0); x++)
                {
                    Cells[x, y] = new Cell();
                }
            }
        }

        public void Clear()
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                for (int x = 0; x < Cells.GetLength(0); x++)
                {
                    Cells[x, y].BlockedByMap = false;
                    Cells[x, y].BlockedByEntity = false;
                    Cells[x, y].OpaqueByMap = false;
                    Cells[x, y].OpaqueByEntity = false;
                    Cells[x, y].Content.Clear();
                }
            }
        }

        public void PopulateBlockedFromMap(MapData mapData)
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                for (int x = 0; x < Cells.GetLength(0); x++)
                {
                    Cells[x, y].BlockedByMap = !mapData[x, y].Type.Walkable();
                }
            }
        }

        public void PopulateOpaqueFromMap(MapData mapData)
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                for (int x = 0; x < Cells.GetLength(0); x++)
                {
                    Cells[x, y].OpaqueByMap = mapData[x, y].Type.Opaque();
                }
            }
        }

        public void IndexEntityAt(GameEntity entity, int x, int y, bool blocksTile, bool opaque)
        {
            Cells[x, y].Content.Add(new CellContent
            {
                BlocksTile = blocksTile,
                Opaque = opaque,
                Entity = entity,
            });
            if (blocksTile)
            {
                Cells[x, y].BlockedByEntity = true;
            }

            if (opaque)
            {
                Cells[x, y].OpaqueByEntity = true;
            }
        }

        public void MoveEntity(GameEntity entity, int fromX, int fromY, int toX, int toY)
        {
            bool entityBlocks = false;
            bool entityOpaque = false;
            Cells[fromX, fromY].Content.RemoveAll(cc =>
            {
                if (cc.Entity == entity)
                {
                    entityBlocks = cc.BlocksTile;
                    entityOpaque = cc.Opaque;
                    return true;
                }

                return false;
            });
            Cells[toX, toY].Content.Add(new CellContent
            {
                BlocksTile = entityBlocks,
                Opaque = entityOpaque,
                Entity = entity,
            });
            
            // Recalculate blocks for both tiles
            
            bool fromBlocked = false;
            bool fromOpaque = false;
            bool toBlocked = false;
            bool toOpaque = false;
            for (int i = 0; i < Cells[fromX, fromY].Content.Count; i++)
            {
                if (Cells[fromX, fromY].Content[i].BlocksTile)
                {
                    fromBlocked = true;
                }
                if (Cells[fromX, fromY].Content[i].Opaque)
                {
                    fromOpaque = true;
                }
            }
            for (int i = 0; i < Cells[toX, toY].Content.Count; i++)
            {
                if (Cells[toX, toY].Content[i].BlocksTile)
                {
                    toBlocked = true;
                }
                if (Cells[toX, toY].Content[i].Opaque)
                {
                    toOpaque = true;
                }
            }

            Cells[fromX, fromY].BlockedByEntity = fromBlocked;
            Cells[fromX, fromY].OpaqueByEntity = fromOpaque;
            Cells[toX, toY].BlockedByEntity = toBlocked;
            Cells[toX, toY].OpaqueByEntity = toOpaque;
        }

        public void RemoveEntityAt(GameEntity entity, int x, int y)
        {
            Cells[x, y].Content.RemoveAll(cc => cc.Entity == entity);
            bool stillBlocked = false;
            bool stillOpaque = false;
            for (int i = 0; i < Cells[x, y].Content.Count; i++)
            {
                if (Cells[x, y].Content[i].BlocksTile)
                {
                    stillBlocked = true;
                }
                if (Cells[x, y].Content[i].Opaque)
                {
                    stillOpaque = true;
                }
            }

            Cells[x, y].BlockedByEntity = stillBlocked;
            Cells[x, y].OpaqueByEntity = stillOpaque;
        }

        private bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < Cells.GetLength(0) &&
                   y >= 0 && y < Cells.GetLength(1);
        }

        public bool IsBlocked(int x, int y)
        {
            if (!IsInBounds(x, y)) return true;
            return Cells[x, y].Blocked;
        }

        public bool IsOpaque(int x, int y)
        {
            if (!IsInBounds(x, y)) return true;
            return Cells[x, y].Opaque;
        }
    }
}