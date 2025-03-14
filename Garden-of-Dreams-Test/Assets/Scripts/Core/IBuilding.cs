using UnityEngine;

namespace Menedment.Core
{
    public interface IBuilding
    {
        string BuildingID { get; }
        Vector2Int Size { get; }
        void Initialize(string id, Vector2Int size);
    }
}