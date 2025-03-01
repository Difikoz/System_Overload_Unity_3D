using UnityEngine;

namespace WinterUniverse
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] protected bool _interactableForAI;
        [SerializeField] protected string _interactionMessage = "Action";
        [SerializeField] protected string _notificationMessage = "Notification";

        public bool InteractableForAI => _interactableForAI;

        public virtual string GetInteractionMessage() => _interactionMessage;
        public virtual string GetNotificationMessage() => _notificationMessage;
        public virtual bool CanInteract(PawnController pawn) => true;
        public abstract void Interact(PawnController pawn);
    }
}