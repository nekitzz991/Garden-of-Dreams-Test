using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Menedment.GridSystem;
using Menedment.Core;

namespace Menedment.Data
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        private string savePath => Path.Combine(Application.persistentDataPath, "buildings.json");
        private List<BuildingData> placedBuildings = new();
        [SerializeField] private BuildingConfig[] buildingConfigs;

        public void Initialize() => Instance = this;

        public void SaveBuilding(Vector2Int pos, string buildingID)
        {
            placedBuildings.Add(new BuildingData { Position = pos, BuildingID = buildingID });
            SaveToFile();
        }

        public void RemoveBuilding(Vector2Int pos)
        {
            placedBuildings.RemoveAll(b => b.Position == pos);
            SaveToFile();
        }

        public void LoadBuildings()
        {
            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                placedBuildings = JsonUtility.FromJson<BuildingDataWrapper>(json).Buildings;
                foreach (var data in placedBuildings)
                {
                    BuildingConfig config = GetBuildingConfigByID(data.BuildingID);
                    Vector3 worldPos = GridManager.Instance.GridToWorld(data.Position);
                    GameObject buildingObj = Instantiate(config.FinalPrefab, worldPos, Quaternion.identity);
                    IBuilding building = buildingObj.GetComponent<IBuilding>();
                    building.Initialize(data.BuildingID, new Vector2Int(1, 1));
                    GridManager.Instance.OccupyCell(data.Position, building);
                }
            }
        }

        private void SaveToFile()
        {
            BuildingDataWrapper wrapper = new BuildingDataWrapper { Buildings = placedBuildings };
            string json = JsonUtility.ToJson(wrapper);
            File.WriteAllText(savePath, json);
        }

        public BuildingConfig GetBuildingConfig(int index) => buildingConfigs[index];
        public BuildingConfig GetBuildingConfigByID(string id) => System.Array.Find(buildingConfigs, c => c.ID == id);

        [System.Serializable]
        private class BuildingDataWrapper
        {
            public List<BuildingData> Buildings;
        }
    }
}