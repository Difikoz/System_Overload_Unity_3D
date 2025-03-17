using UnityEngine;

namespace WinterUniverse
{
    public class PlayerController
    {
        private PlayerInputActions _inputActions;
        private PawnController _pawn;
        private Vector2 _moveInput;

        public PawnController Pawn => _pawn;

        public PlayerController()
        {
            _inputActions = new();
            _pawn = new();
        }

        public void Enable()
        {
            _inputActions.Enable();
        }

        public void Disable()
        {
            _inputActions.Disable();
        }

        public void OnUpdate(float deltaTime)
        {
            _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
            _pawn.Locomotion.MoveDirection = GameManager.StaticInstance.CameraManager.transform.forward * _moveInput.y + GameManager.StaticInstance.CameraManager.transform.right * _moveInput.x;
            _pawn.Locomotion.LookDirection = GameManager.StaticInstance.CameraManager.transform.forward;
            _pawn.OnUpdate(deltaTime);
        }
    }
}