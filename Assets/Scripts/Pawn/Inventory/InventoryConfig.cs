using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Winter Universe/Pawn/New Inventory")]
    public class InventoryConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private List<ItemStack> _itemStacks = new();

        public string ID => _id;
        public List<ItemStack> ItemStacks => _itemStacks;
    }
}