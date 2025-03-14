using UnityEngine;

namespace Menedment.GridSystem
{
    public class GridVisualizer : MonoBehaviour
    {
        [SerializeField] private GridManager gridManager;
        [SerializeField] private float lineThickness = 0.1f;

        void OnDrawGizmos()
        {
            if (gridManager == null) return;

            Vector2Int gridSize = new Vector2Int(10, 10); // Должно соответствовать инициализации
            float cellSize = 1f;

            Gizmos.color = Color.gray;
            for (int x = 0; x <= gridSize.x; x++)
            {
                Vector3 start = new Vector3(x * cellSize, 0, 0);
                Vector3 end = new Vector3(x * cellSize, gridSize.y * cellSize, 0);
                Gizmos.DrawLine(start, end);
            }

            for (int y = 0; y <= gridSize.y; y++)
            {
                Vector3 start = new Vector3(0, y * cellSize, 0);
                Vector3 end = new Vector3(gridSize.x * cellSize, y * cellSize, 0);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}