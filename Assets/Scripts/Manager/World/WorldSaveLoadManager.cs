using UnityEngine;

namespace WinterUniverse
{
    public class WorldSaveLoadManager : MonoBehaviour
    {
        [SerializeField] private string _fileName = "SaveData";
        [SerializeField] private PawnConfig _playerConfig;
        public PawnSaveData CurrentSaveData;

        public void SaveGame()
        {
            GameManager.StaticInstance.PlayerManager.SaveData(ref CurrentSaveData);
            DataWriter.CreateSaveFile(CurrentSaveData, _fileName);
        }

        public void LoadGame()
        {
            if (DataWriter.FileExists(_fileName))
            {
                CurrentSaveData = DataWriter.LoadSavedFile(_fileName);
            }
            else
            {
                CurrentSaveData = _playerConfig.GetData();
                GameManager.StaticInstance.PlayerManager.Pawn.CreatePawn(CurrentSaveData);
                SaveGame();
            }
            GameManager.StaticInstance.PlayerManager.LoadData(CurrentSaveData);
        }

        public void DeleteGame()
        {
            //DataWriter.DeleteSavedFile(_fileName);
        }
    }
}