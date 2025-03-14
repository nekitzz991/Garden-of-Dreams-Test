using UnityEngine;

namespace Menedment.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera; 

        void Awake()
        {
            Menedment.GridSystem.GridManager.Instance.Initialize(10, 10, 1f);
            Menedment.InputSystem.InputHandler.Instance.Initialize(mainCamera);
            Menedment.UI.UIManager.Instance.Initialize();
            Menedment.BuildingSystem.BuildingPlacer.Instance.Initialize();
            Menedment.Data.DataManager.Instance.LoadBuildings();

            // Передаем IUIManager в BuildingPlacer
            Menedment.BuildingSystem.BuildingPlacer.Instance.SetUIManager(Menedment.UI.UIManager.Instance);
        }
    }
}