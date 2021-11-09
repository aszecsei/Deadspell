using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Core
{
    public class GridPosition : SerializedMonoBehaviour
    {
        public Vector2Int Position;

        private void Update()
        {
            transform.position = new Vector3(Position.x, Position.y) + TilemapManager.Instance.Offset;
        }
    }
}