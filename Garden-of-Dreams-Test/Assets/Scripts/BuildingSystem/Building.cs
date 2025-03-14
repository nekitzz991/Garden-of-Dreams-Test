using UnityEngine;

namespace Menedment.BuildingSystem
{
    public class Building : MonoBehaviour, Menedment.Core.IBuilding
    {
        public string BuildingID { get; private set; }
        public Vector2Int Size { get; private set; }

        public void Initialize(string id, Vector2Int size)
        {
            BuildingID = id;
            Size = size;
        }
    }
}