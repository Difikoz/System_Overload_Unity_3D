using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private AudioManager _audioManager;
        private CameraManager _cameraManager;
        private ConfigsManager _configsManager;
        private ControllersManager _controllersManager;
        private LayersManager _layersManager;
        private PrefabsManager _prefabsManager;
        private UIManager _uiManager;

        public AudioManager AudioManager => _audioManager;
        public CameraManager CameraManager => _cameraManager;
        public ConfigsManager ConfigsManager => _configsManager;
        public ControllersManager ControllersManager => _controllersManager;
        public LayersManager LayersManager => _layersManager;
        public PrefabsManager PrefabsManager => _prefabsManager;
        public UIManager UIManager => _uiManager;

        protected override void Awake()
        {
            base.Awake();
            GetComponents();
        }

        private void Start()
        {
            InitializeComponents();
        }

        private void Update()
        {
            UpdateComponents(Time.deltaTime);
        }

        private void GetComponents()
        {
            _audioManager = GetComponentInChildren<AudioManager>();
            _cameraManager = GetComponentInChildren<CameraManager>();
            _configsManager = GetComponentInChildren<ConfigsManager>();
            _controllersManager = GetComponentInChildren<ControllersManager>();
            _layersManager = GetComponentInChildren<LayersManager>();
            _prefabsManager = GetComponentInChildren<PrefabsManager>();
            _uiManager = GetComponentInChildren<UIManager>();
        }

        private void InitializeComponents()
        {
            _configsManager.InitializeComponent();
            _audioManager.InitializeComponent();
            _cameraManager.InitializeComponent();
            //_controllersManager.InitializeComponent();
            //_uiManager.InitializeComponent();
        }

        private void UpdateComponents(float deltaTime)
        {
            _cameraManager.OnUpdate(deltaTime);
            //_controllersManager.OnUpdate(deltaTime);
        }
    }
}