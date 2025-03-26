using UnityEngine;

namespace WinterUniverse
{
    public class CameraController : BasicComponent
    {
        [SerializeField] private float _followSpeed = 10f;
        [SerializeField] private Transform _rotationRoot;
        [SerializeField] private float _thirdPersonRotateSpeed = 20f;
        [SerializeField] private float _firstPersonRotateSpeed = 10f;
        [SerializeField] private float _minAngle = 45f;
        [SerializeField] private float _maxAngle = 45f;
        [SerializeField] private Transform _collisionRoot;
        [SerializeField] private float _collisionRadius = 0.25f;
        [SerializeField] private float _collisionAvoidanceSpeed = 10f;
        [SerializeField] private LayerMask _obstacleMask;

        private PlayerController _player;
        private PlayerInputActions _inputActions;
        private Vector2 _lookInput;
        private float _rotateSpeed;
        private float _xRot;
        private Vector3 _collisionCurrentOffset;
        private float _collisionDefaultOffset;
        private float _collisionRequiredOffset;
        private RaycastHit _collisionHit;

        public Camera Camera { get; private set; }
        public CameraViewMode ViewMode { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            Camera = GetComponentInChildren<Camera>();
            _inputActions = new();
            _player = GameManager.StaticInstance.Player;
            _rotateSpeed = _thirdPersonRotateSpeed;
        }

        public override void Enable()
        {
            base.Initialize();
            _inputActions.Enable();
            _inputActions.Camera.Previous.performed += ctx => ToggleViewMode();
            _xRot = _rotationRoot.localEulerAngles.x;
            _collisionDefaultOffset = _collisionRoot.localPosition.z;
        }

        public override void Disable()
        {
            _inputActions.Camera.Previous.performed -= ctx => ToggleViewMode();
            _inputActions.Disable();
            base.Disable();
        }

        public override void OnUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _player.transform.position, _followSpeed * Time.deltaTime);
            LookAround();
            HandleCollision();
            Camera.transform.localPosition = Vector3.Lerp(Camera.transform.localPosition, Vector3.zero, _followSpeed * Time.deltaTime);
        }

        private void LookAround()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            _lookInput = _inputActions.Camera.Look.ReadValue<Vector2>();
            if (_lookInput.x != 0f)
            {
                transform.Rotate(Vector3.up * _lookInput.x * _rotateSpeed * Time.deltaTime);
            }
            if (_lookInput.y != 0f)
            {
                _xRot = Mathf.Clamp(_xRot - (_lookInput.y * _rotateSpeed * Time.deltaTime), -_maxAngle, _minAngle);
                _rotationRoot.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
                if (ViewMode == CameraViewMode.ThirdPerson)
                {
                    Camera.transform.localRotation = Quaternion.Slerp(Camera.transform.localRotation, Quaternion.identity, _followSpeed * Time.deltaTime);
                }
                else if (ViewMode == CameraViewMode.FirstPerson)
                {
                    Camera.transform.localRotation = Quaternion.Slerp(Camera.transform.localRotation, _rotationRoot.localRotation, _followSpeed * Time.deltaTime);
                }
            }
        }

        private void HandleCollision()
        {
            _collisionRequiredOffset = _collisionDefaultOffset;
            Vector3 direction = (_collisionRoot.position - _rotationRoot.position).normalized;
            if (Physics.SphereCast(_rotationRoot.position, _collisionRadius, direction, out _collisionHit, Mathf.Abs(_collisionRequiredOffset), _obstacleMask))
            {
                _collisionRequiredOffset = -(Vector3.Distance(_rotationRoot.position, _collisionHit.point) - _collisionRadius);
            }
            if (Mathf.Abs(_collisionRequiredOffset) < _collisionRadius)
            {
                _collisionRequiredOffset = -_collisionRadius;
            }
            _collisionCurrentOffset.z = Mathf.Lerp(_collisionRoot.localPosition.z, _collisionRequiredOffset, _collisionAvoidanceSpeed * Time.deltaTime);
            _collisionRoot.localPosition = _collisionCurrentOffset;
        }

        public void ToggleViewMode()
        {
            if (ViewMode == CameraViewMode.ThirdPerson)
            {
                _rotateSpeed = _firstPersonRotateSpeed;
                ViewMode = CameraViewMode.FirstPerson;
                Camera.transform.parent = _player.Animator.Eyes;
            }
            else if (ViewMode == CameraViewMode.FirstPerson)
            {
                _rotateSpeed = _thirdPersonRotateSpeed;
                ViewMode = CameraViewMode.ThirdPerson;
                Camera.transform.parent = _collisionRoot;
            }
        }
    }
}