using UnityEngine;

namespace Menedment.UI
{
    public class UIManager : MonoBehaviour, Menedment.Core.IUIManager
    {
        public static UIManager Instance { get; private set; }
        private bool isRemoving;

        public bool IsRemoving => isRemoving;

        public void Initialize()
        {
            Instance = this;
            isRemoving = false;
        }

        public void SetRemoveMode(bool value) => isRemoving = value;
        public void ResetRemoveMode() => isRemoving = false;
    }
}