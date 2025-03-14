using UnityEngine;
using System.Collections.Generic;
using Menedment.Core;

namespace Menedment.GridSystem
{
    public class GridManager : MonoBehaviour
    {
        public static GridManager Instance { get; private set; }
        private Vector2Int gridSize;
        private float cellSize;
        private Dictionary<Vector2Int, IBuilding> occupiedCells = new();

        public void Initialize(int width, int height, float size)
        {
            Instance = this;
            gridSize = new Vector2Int(width, height);
            cellSize = size;
        }

        public Vector3 SnapToGrid(Vector3 worldPosition)
        {
            Vector2Int gridPos = WorldToGrid(worldPosition);
            return GridToWorld(gridPos);
        }

        public Vector2Int WorldToGrid(Vector3 worldPosition)
        {
            int x = Mathf.FloorToInt(worldPosition.x / cellSize);
            int y = Mathf.FloorToInt(worldPosition.y / cellSize);
            return new Vector2Int(x, y);
        }

        public Vector3 GridToWorld(Vector2Int gridPosition)
        {
            float x = gridPosition.x * cellSize + cellSize / 2;
            float y = gridPosition.y * cellSize + cellSize / 2;
            return new Vector3(x, y, 0);
        }

        public bool IsCellOccupied(Vector2Int gridPos) => occupiedCells.ContainsKey(gridPos);
        public void OccupyCell(Vector2Int gridPos, IBuilding building) => occupiedCells[gridPos] = building;
        public void FreeCell(Vector2Int gridPos) => occupiedCells.Remove(gridPos);
        public IBuilding GetBuildingAt(Vector2Int gridPos) => occupiedCells.GetValueOrDefault(gridPos);
    }
}