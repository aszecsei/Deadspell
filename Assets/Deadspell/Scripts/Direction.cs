using System;
using UnityEngine;

namespace Deadspell
{
    [Flags]
    public enum Direction : byte
    {
        North = 1 << 1,
        East = 1 << 2,
        South = 1 << 3,
        West = 1 << 4,
        NorthWest = North | West,
        NorthEast = North | East,
        SouthWest = South | West,
        SouthEast = South | East,
    }

    public static class DirectionExt
    {
        private static bool IsFlagSet(this Direction dir, Direction other)
        {
            return (dir & other) == other;
        }
        public static Vector2Int ToVector2Int(this Direction dir)
        {
            Vector2Int result = Vector2Int.zero;
            if (dir.IsFlagSet(Direction.West))
            {
                result += Vector2Int.left;
            }
            if (dir.IsFlagSet(Direction.East))
            {
                result += Vector2Int.right;
            }
            if (dir.IsFlagSet(Direction.North))
            {
                result += Vector2Int.up;
            }
            if (dir.IsFlagSet(Direction.South))
            {
                result += Vector2Int.down;
            }

            return result;
        }
    }
}