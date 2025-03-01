using System.Collections;
using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private bool _clearSaveData;

        private PlayerController _playerManager;
        private WorldCameraManager _cameraManager;
        private WorldUIManager _uiManager;
        private WorldLayerManager _layerManager;
        private WorldConfigsManager _configsManager;
        private WorldObjectManager _objectManager;
        private WorldSaveLoadManager _saveLoadManager;
        private WorldSoundManager _soundManager;
        private WorldTimeManager _timeManager;

        private bool _completed;

        public PlayerController PlayerManager => _playerManager;
        public WorldCameraManager CameraManager => _cameraManager;
        public WorldUIManager UIManager => _uiManager;
        public WorldLayerManager LayerManager => _layerManager;
        public WorldConfigsManager ConfigsManager => _configsManager;
        public WorldObjectManager ObjectManager => _objectManager;
        public WorldSaveLoadManager SaveLoadManager => _saveLoadManager;
        public WorldSoundManager SoundManager => _soundManager;
        public WorldTimeManager TimeManager => _timeManager;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(LoadingTimer());
        }

        private IEnumerator LoadingTimer()
        {
            WaitForSeconds delay = new(0.25f);
            yield return null;
            _playerManager = GetComponentInChildren<PlayerController>();
            _cameraManager = GetComponentInChildren<WorldCameraManager>();
            _uiManager = GetComponentInChildren<WorldUIManager>();
            _layerManager = GetComponentInChildren<WorldLayerManager>();
            _configsManager = GetComponentInChildren<WorldConfigsManager>();
            _objectManager = GetComponentInChildren<WorldObjectManager>();
            _saveLoadManager = GetComponentInChildren<WorldSaveLoadManager>();
            _soundManager = GetComponentInChildren<WorldSoundManager>();
            _timeManager = GetComponentInChildren<WorldTimeManager>();
            yield return null;
            _uiManager.LoadingScreenUI.Show();
            yield return null;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize World Data", 0, 1);
            _configsManager.Initialize();
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize World Data", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize World Sound", 0, 1);
            _soundManager.Initialize();
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize World Sound", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize World Time", 0, 1);
            _timeManager.Initialize();
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize World Time", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player", 0, 1);
            _playerManager.Initialize();
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Input", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Camera", 0, 1);
            _cameraManager.Initialize();
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player Camera", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player UI", 0, 1);
            _uiManager.Initialize();
            yield return delay;
            _uiManager.MenuUI.CloseMenu();
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Initialize Player UI", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Load Saved Data", 0, 1);
            _saveLoadManager.LoadGame();
            yield return delay;
            _uiManager.LoadingScreenUI.UpdateLoadingScreen("Load Saved Data", 1, 1);
            yield return delay;
            _uiManager.LoadingScreenUI.Hide();
            Debug.Log($"Loaded : {Time.timeSinceLevelLoad} seconds.");
            yield return null;
            _completed = true;
        }

        private void Update()
        {
            if (!_completed)
            {
                return;
            }
            _playerManager.OnUpdate();
            _timeManager.OnUpdate();
            if (_clearSaveData)
            {
                _clearSaveData = false;
                _saveLoadManager.DeleteGame();
            }
        }

        private void LateUpdate()
        {
            if (!_completed)
            {
                return;
            }
            _cameraManager.OnLateUpdate();
        }
    }
}