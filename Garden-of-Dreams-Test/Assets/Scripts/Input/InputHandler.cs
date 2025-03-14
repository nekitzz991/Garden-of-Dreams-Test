using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace Menedment.InputSystem
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler Instance { get; private set; }
        private PlayerInputActions inputActions;
        private Camera mainCamera;

        public event Action<Vector3> OnClickPerformed;

        public void Initialize(Camera camera)
        {
            Instance = this;
            mainCamera = camera;
            inputActions = new PlayerInputActions();
            inputActions.Gameplay.Enable();
            inputActions.Gameplay.Click.performed += ctx => OnClick();
        }

        public Vector3 GetMousePosition()
        {
            Vector2 screenPos = inputActions.Gameplay.MousePosition.ReadValue<Vector2>();
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10));
            worldPos.z = 0;
            return worldPos;
        }

        private void OnClick()
        {
            Vector3 clickPos = GetMousePosition();
            OnClickPerformed?.Invoke(clickPos);
        }

        void OnDestroy()
        {
            inputActions.Gameplay.Disable();
        }
    }
}