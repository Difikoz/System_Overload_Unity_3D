using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class InteractableChest : Interactable
    {
        [SerializeField] private List<ItemStack> _stacks = new();

        public List<ItemStack> Items => _stacks;

        public override void Interact(PawnController pawn)
        {
            foreach (ItemStack stack in _stacks)
            {
                pawn.PawnInventory.AddItem(stack.Item, stack.Amount);
                if (pawn.CompareTag("Player"))
                {
                    GameManager.StaticInstance.UIManager.NotificationUI.DisplayNotification($"{_notificationMessage} {stack.Amount} {stack.Item.DisplayName}");
                }
            }
        }
    }
}