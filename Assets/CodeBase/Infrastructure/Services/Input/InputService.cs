using CodeBase.Infrastructure.Services.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.CodeBase.Services.Input
{
    public class InputService : IInputService, IDisposable
    {
        public event Action<Vector3> ClickPosition;
        private ActionMap inputActions;

        public InputService()
        {
            inputActions = new ActionMap();
            inputActions.Enable();
            inputActions.Player.Click.performed += OnClick;
            inputActions.Player.Touch.performed += OnTouch;
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            var position = Mouse.current.position.ReadValue();
            ClickPosition?.Invoke(position);
        }

        private void OnTouch(InputAction.CallbackContext context)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            ClickPosition?.Invoke(touchPosition);
        }

        public void Dispose()
        {
            inputActions.Player.Click.performed -= OnClick;
            inputActions.Player.Touch.performed -= OnTouch;
            inputActions.Disable();
        }

        public void Enable()
        {
            inputActions.Enable();
        }

        public void Disable()
        {
            inputActions.Disable();
        }
    }
}
