using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class InteractableItem : Interactable
    {
        private ItemConfig _config;
        private int _amount;
        private GameObject _model;

        public void Setup(ItemConfig data, int amount)
        {
            _config = data;
            _amount = amount;
            if (_model != null)
            {
                LeanPool.Despawn(_model);
            }
            _model = LeanPool.Spawn(_config.Model, transform);
        }

        public override string GetInteractionMessage()
        {
            return $"{_interactionMessage} {_amount} {_config.DisplayName}";
        }

        public override string GetNotificationMessage()
        {
            return $"{_notificationMessage} {_amount} {_config.DisplayName}";
        }

        public override void Interact(PawnController pawn)
        {
            pawn.PawnInventory.AddItem(_config, _amount);
            if (pawn.CompareTag("Player"))
            {
                GameManager.StaticInstance.UIManager.NotificationUI.DisplayNotification(GetNotificationMessage());
            }
            LeanPool.Despawn(gameObject);
        }
    }
}