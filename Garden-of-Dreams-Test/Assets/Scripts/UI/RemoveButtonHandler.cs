using UnityEngine;
using UnityEngine.UI;

namespace Menedment.UI
{
    public class RemoveButtonHandler : MonoBehaviour
    {
        private Button button;

        void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(ToggleRemoveMode);
        }

        private void ToggleRemoveMode()
        {
            Menedment.UI.UIManager.Instance.SetRemoveMode(!Menedment.UI.UIManager.Instance.IsRemoving);
        }
    }
}