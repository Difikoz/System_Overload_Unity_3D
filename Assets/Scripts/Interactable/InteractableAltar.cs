using UnityEngine;

namespace WinterUniverse
{
    public class InteractableAltar : Interactable
    {
        [SerializeField] private Transform _respawnPoint;

        public override void Interact(PawnController pawn)
        {
            pawn.PawnEffects.RemoveNegativeEffects();
            pawn.PawnStats.RestoreCurrentHealth(pawn.PawnStats.HealthMax.CurrentValue);
            pawn.PawnStats.RestoreCurrentEnergy(pawn.PawnStats.EnergyMax.CurrentValue);
            GameManager.StaticInstance.SaveLoadManager.CurrentSaveData.RespawnTransform.SetPositionAndRotation(_respawnPoint.position, _respawnPoint.eulerAngles);
            GameManager.StaticInstance.SaveLoadManager.SaveGame();
            GameManager.StaticInstance.UIManager.NotificationUI.DisplayNotification(_notificationMessage);
        }
    }
}