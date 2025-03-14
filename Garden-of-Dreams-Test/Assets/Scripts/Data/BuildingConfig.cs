using UnityEngine;

namespace Menedment.Data
{
    [CreateAssetMenu(fileName = "BuildingConfig", menuName = "Configs/Building")]
    public class BuildingConfig : ScriptableObject
    {
        public string ID;
        public GameObject PreviewPrefab; // Префаб для предпросмотра
        public GameObject FinalPrefab;   // Окончательный префаб здания
    }
}