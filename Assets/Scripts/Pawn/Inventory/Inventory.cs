using System;
using System.Collections.Generic;

namespace WinterUniverse
{
    public class Inventory
    {
        public Action OnInventoryChanged;

        private List<ItemStack> _stacks;

        public List<ItemStack> Stacks => _stacks;

        public Inventory()
        {
            _stacks = new();
        }

        public void AddItem(ItemStack stack)
        {
            AddItem(stack.Item, stack.Amount);
        }

        public void AddItem(ItemConfig item, int amount = 1)
        {
            if (item.StackSize > 1)
            {
                foreach (ItemStack stack in _stacks)
                {
                    if (stack.Item.DisplayName == item.DisplayName)
                    {
                        while (stack.HasFreeSpace() && amount > 0)
                        {
                            stack.AddToStack();
                            amount--;
                        }
                        if (amount <= 0)
                        {
                            break;
                        }
                    }
                }
                while (amount > 0)
                {
                    ItemStack stack = new(item, 0);
                    while (stack.HasFreeSpace() && amount > 0)
                    {
                        stack.AddToStack();
                        amount--;
                    }
                    _stacks.Add(stack);
                }
            }
            else
            {
                while (amount > 0)
                {
                    _stacks.Add(new(item));
                    amount--;
                }
            }
            UpdateInventory();
        }

        public bool RemoveItem(ItemStack stack)
        {
            return RemoveItem(stack.Item, stack.Amount);
        }

        public bool RemoveItem(ItemConfig item, int amount = 1)
        {
            if (AmountOfItem(item) < amount)
            {
                return false;
            }
            for (int i = _stacks.Count - 1; i >= 0; i--)
            {
                if (_stacks[i].Item.DisplayName == item.DisplayName)
                {
                    while (_stacks[i].Amount > 0 && amount > 0)
                    {
                        _stacks[i].RemoveFromStack();
                        amount--;
                    }
                    if (_stacks[i].Amount <= 0)
                    {
                        _stacks.RemoveAt(i);
                    }
                    if (amount <= 0)
                    {
                        break;
                    }
                }
            }
            UpdateInventory();
            return true;
        }

        public bool DropItem(ItemStack stack)
        {
            return DropItem(stack.Item, stack.Amount);
        }

        public bool DropItem(ItemConfig item, int amount = 1)
        {
            if (AmountOfItem(item) < amount)
            {
                return false;
            }
            for (int i = _stacks.Count - 1; i >= 0; i--)
            {
                if (_stacks[i].Item.DisplayName == item.DisplayName)
                {
                    if (_stacks[i].Amount < amount)
                    {
                        amount -= _stacks[i].Amount;
                        //GameManager.StaticInstance.PrefabsManager.GetItem(transform.position, Quaternion.identity).Initialize(item, _stacks[i].Amount);
                        _stacks.RemoveAt(i);
                    }
                    else
                    {
                        //GameManager.StaticInstance.PrefabsManager.GetItem(transform.position, Quaternion.identity).Initialize(item, amount);
                        _stacks[i].RemoveFromStack(amount);
                        if (_stacks[i].Amount == 0)
                        {
                            _stacks.RemoveAt(i);
                        }
                        break;
                    }
                }
            }
            UpdateInventory();
            return true;
        }

        public int AmountOfItem(ItemConfig item)
        {
            int amount = 0;
            foreach (ItemStack stack in _stacks)
            {
                if (stack.Item.DisplayName == item.DisplayName)
                {
                    amount += stack.Amount;
                }
            }
            return amount;
        }

        public bool GetWeapon(out WeaponItemConfig item)
        {
            item = null;
            int price = 0;
            foreach (ItemStack stack in _stacks)
            {
                if (stack.Item.ItemType == ItemType.Weapon && stack.Item.Price > price)
                {
                    item = (WeaponItemConfig)stack.Item;
                    price = item.Price;
                }
            }
            return item != null;
        }

        public bool GetArmor(out ArmorItemConfig item)
        {
            item = null;
            int price = 0;
            foreach (ItemStack stack in _stacks)
            {
                if (stack.Item.ItemType == ItemType.Armor && stack.Item.Price > price)
                {
                    item = (ArmorItemConfig)stack.Item;
                    price = item.Price;
                }
            }
            return item != null;
        }

        public bool GetConsumable(out ConsumableItemConfig item)
        {
            item = null;
            int price = 0;
            foreach (ItemStack stack in _stacks)
            {
                if (stack.Item.ItemType == ItemType.Consumable && stack.Item.Price > price)
                {
                    item = (ConsumableItemConfig)stack.Item;
                    price = item.Price;
                }
            }
            return item != null;
        }

        private void UpdateInventory()
        {
            OnInventoryChanged?.Invoke();
        }
    }
}