using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class ItemStack
    {
        [SerializeField] private ItemConfig _item;
        [SerializeField] private int _amount;

        public ItemConfig Item => _item;
        public int Amount => _amount;

        public ItemStack(ItemConfig item, int amount = 1)
        {
            _item = item;
            _amount = amount;
        }

        public bool HasFreeSpace()
        {
            return _amount < _item.StackSize;
        }

        public void AddToStack(int amount = 1)
        {
            _amount += amount;
        }

        public void RemoveFromStack(int amount = 1)
        {
            _amount -= amount;
        }
    }
}