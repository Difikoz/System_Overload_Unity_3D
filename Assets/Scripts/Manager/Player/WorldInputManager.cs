using UnityEngine;

namespace WinterUniverse
{
    public class WorldInputManager : MonoBehaviour
    {
        private PlayerInputActions _inputActions;
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private bool _runInput;
        private bool _jumpInput;
        private bool _interactInput;
        private bool _actionMainInput;
        private bool _actionSecondInput;

        public Vector2 MoveInput => _moveInput;
        public Vector2 LookInput => _lookInput;
        public bool RunInput => _runInput;
        public bool JumpInput => _jumpInput;
        public bool InteractInput => _interactInput;
        public bool ActionMainInput => _actionMainInput;
        public bool ActionSecondInput => _actionSecondInput;

        public void Initialize()
        {
            _inputActions = new();
            _inputActions.Enable();
        }

        public void OnUpdate()
        {
            _moveInput = _inputActions.Pawn.Move.ReadValue<Vector2>();
            _lookInput = _inputActions.Pawn.Look.ReadValue<Vector2>();
            _runInput = _inputActions.Pawn.Run.IsPressed();
            _interactInput = _inputActions.Pawn.Interact.IsPressed();
            _jumpInput = _inputActions.Pawn.Jump.IsPressed();
            _actionMainInput = _inputActions.Pawn.ActionMainHand.IsPressed();
            _actionSecondInput = _inputActions.Pawn.ActionSecondHand.IsPressed();
        }
    }
}