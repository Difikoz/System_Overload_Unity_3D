using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Pawn", menuName = "Winter Universe/Pawn/New Pawn")]
    public class PawnConfig : ScriptableObject
    {
        public string PawnName = "Name";
        public FactionConfig Faction;
        public WeaponItemConfig WeaponInRightHand;
        public WeaponItemConfig WeaponInLeftHand;
        public List<ArmorItemConfig> Armors = new();
        public List<ItemStack> StartingItems = new();

        public PawnSaveData GetData()
        {
            PawnSaveData data = new()
            {
                PawnName = PawnName,
                Faction = Faction.DisplayName,
                WeaponInRightHand = WeaponInRightHand.DisplayName,
                WeaponInLeftHand = WeaponInLeftHand.DisplayName,
            };
            data.Armors.Clear();
            foreach (ArmorItemConfig armor in Armors)
            {
                data.Armors.Add(armor.DisplayName);
            }
            data.InventoryStacks.Clear();
            foreach (ItemStack stack in StartingItems)
            {
                if (data.InventoryStacks.ContainsKey(stack.Item.DisplayName))
                {
                    data.InventoryStacks[stack.Item.DisplayName] += stack.Amount;
                }
                else
                {
                    data.InventoryStacks.Add(stack.Item.DisplayName, stack.Amount);
                }
            }
            return data;
        }
    }
}