using UnityEngine;
using UnityEngine.UI;
using Menedment.Data;

namespace Menedment.UI
{
    public class UIBuildingSelector : MonoBehaviour
    {
        [SerializeField] private Button Button1;
        [SerializeField] private Button Button2;
        [SerializeField] private Button Button3;
        [SerializeField] private Button placeButton;
        [SerializeField] private Button removeButton;

        void Awake()
        {
            Button1.onClick.AddListener(() => SelectBuilding(0));
            Button2.onClick.AddListener(() => SelectBuilding(1));
            Button3.onClick.AddListener(() => SelectBuilding(2));
            placeButton.onClick.AddListener(OnPlaceClicked);
            removeButton.onClick.AddListener(OnRemoveClicked);
        }

        private void SelectBuilding(int index)
        {
            BuildingConfig config = Menedment.Data.DataManager.Instance.GetBuildingConfig(index);
            Menedment.BuildingSystem.BuildingPlacer.Instance.SelectBuilding(config);
        }

        private void OnPlaceClicked()
        {
            Menedment.UI.UIManager.Instance.SetRemoveMode(false);
        }

        private void OnRemoveClicked()
        {
            Menedment.UI.UIManager.Instance.SetRemoveMode(true);
        }
    }
}