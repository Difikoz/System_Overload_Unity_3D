using UnityEngine;

namespace WinterUniverse
{
    public class PlayerController : PawnController
    {
        [SerializeField] private bool _holdToSprint = true;

        private CameraController _camera;
        private PlayerInputActions _inputActions;
        private Vector2 _moveInput;

        public override void Initialize()
        {
            base.Initialize();
            _inputActions = new();
            _camera = GameManager.StaticInstance.Camera;
        }

        public override void Enable()
        {
            base.Enable();
            _inputActions.Enable();
            _inputActions.Player.Sprint.performed += ctx => OnSprintPerfomed();
            _inputActions.Player.Sprint.canceled += ctx => OnSprintCanceled();
        }

        public override void Disable()
        {
            _inputActions.Player.Sprint.performed -= ctx => OnSprintPerfomed();
            _inputActions.Player.Sprint.canceled -= ctx => OnSprintCanceled();
            _inputActions.Disable();
            base.Disable();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                Locomotion.MoveDirection = Vector3.zero;
                return;
            }
            _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
            Locomotion.MoveDirection = _camera.transform.forward * _moveInput.y + _camera.transform.right * _moveInput.x;
            Locomotion.LookDirection = _camera.transform.forward;
        }

        private void OnSprintPerfomed()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            if (_holdToSprint)
            {
                Locomotion.StartSprint();
            }
            else
            {
                Locomotion.ToggleSprint();
            }
        }

        private void OnSprintCanceled()
        {
            if (_holdToSprint)
            {
                Locomotion.StopSprint();
            }
        }
    }
}