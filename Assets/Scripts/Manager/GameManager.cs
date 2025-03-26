using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        public CameraController Camera { get; private set; }
        public PlayerController Player { get; private set; }
        public InputMode InputMode { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            GetComponents();
        }

        private void Start()
        {
            InitializeComponents();
            EnableComponents();
        }

        private void OnDestroy()
        {
            DisableComponents();
            DestroyComponents();
        }

        private void Update()
        {
            UpdateComponents();
        }

        private void GetComponents()
        {
            Camera = FindFirstObjectByType<CameraController>();
            Player = FindFirstObjectByType<PlayerController>();
        }

        private void InitializeComponents()
        {
            Player.Initialize();
            Camera.Initialize();
            ToggleInputMode(InputMode.Game);
        }

        private void DestroyComponents()
        {
            Player.Destroy();
            Camera.Destroy();
        }

        private void EnableComponents()
        {
            Camera.Enable();
            Player.Enable();
        }

        private void DisableComponents()
        {
            Camera.Disable();
            Player.Disable();
        }

        private void UpdateComponents()
        {
            Camera.OnUpdate();
            Player.OnUpdate();
        }

        public void ToggleInputMode(InputMode mode)
        {
            InputMode = mode;
            if (InputMode == InputMode.Game)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (InputMode == InputMode.UI)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }
}