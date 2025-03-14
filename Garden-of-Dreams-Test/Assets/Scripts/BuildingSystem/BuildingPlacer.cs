using UnityEngine;
using Menedment.Data;
using Menedment.InputSystem;
using Menedment.Core;

namespace Menedment.BuildingSystem
{
    public class BuildingPlacer : MonoBehaviour
    {
        public static BuildingPlacer Instance { get; private set; }
        private BuildingConfig activeBuilding;
        private GameObject previewObject;
        private bool isPlacing;
        private IUIManager uiManager; // Поле для хранения IUIManager

        public void Initialize()
        {
            Instance = this;
            Menedment.InputSystem.InputHandler.Instance.OnClickPerformed += HandleClick;
        }

        public void SetUIManager(IUIManager manager)
        {
            uiManager = manager;
        }

        public void SelectBuilding(BuildingConfig config)
        {
            if (previewObject != null) Destroy(previewObject);
            activeBuilding = config;
            previewObject = Instantiate(config.PreviewPrefab);
            previewObject.transform.position = Vector3.zero;
            isPlacing = true;
        }

        void Update()
        {
            if (isPlacing && previewObject != null)
            {
                Vector3 mousePos = Menedment.InputSystem.InputHandler.Instance.GetMousePosition();
                previewObject.transform.position = Menedment.GridSystem.GridManager.Instance.SnapToGrid(mousePos);
            }
        }

        public void PlaceBuilding()
        {
            if (isPlacing && activeBuilding != null)
            {
                Vector2Int gridPos = Menedment.GridSystem.GridManager.Instance.WorldToGrid(previewObject.transform.position);
                if (!Menedment.GridSystem.GridManager.Instance.IsCellOccupied(gridPos))
                {
                    GameObject buildingObj = Instantiate(activeBuilding.FinalPrefab, previewObject.transform.position, Quaternion.identity);
                    IBuilding buildingInterface = buildingObj.GetComponent<IBuilding>();
                    Building building = buildingInterface as Building;
                    if (building != null)
                    {
                        building.Initialize(activeBuilding.ID, new Vector2Int(1, 1));
                        Menedment.GridSystem.GridManager.Instance.OccupyCell(gridPos, building);
                        Menedment.Data.DataManager.Instance.SaveBuilding(gridPos, activeBuilding.ID);
                        Destroy(previewObject);
                        isPlacing = false;
                    }
                }
            }
        }

        private void HandleClick(Vector3 clickPos)
        {
            if (uiManager != null && uiManager.IsRemoving)
            {
                RemoveBuilding(clickPos);
                uiManager.ResetRemoveMode();
            }
            else if (isPlacing)
            {
                PlaceBuilding();
            }
        }

        public void RemoveBuilding(Vector3 worldPos)
        {
            Vector2Int gridPos = Menedment.GridSystem.GridManager.Instance.WorldToGrid(worldPos);
            if (Menedment.GridSystem.GridManager.Instance.IsCellOccupied(gridPos))
            {
                IBuilding buildingInterface = Menedment.GridSystem.GridManager.Instance.GetBuildingAt(gridPos);
                Building building = buildingInterface as Building;
                if (building != null)
                {
                    Destroy(building.gameObject);
                    Menedment.GridSystem.GridManager.Instance.FreeCell(gridPos);
                    Menedment.Data.DataManager.Instance.RemoveBuilding(gridPos);
                }
            }
        }
    }
}