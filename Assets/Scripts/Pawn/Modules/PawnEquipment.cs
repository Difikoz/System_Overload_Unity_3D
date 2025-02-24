using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipment : MonoBehaviour
    {
        public Action OnEquipmentChanged;

        private PawnController _pawn;

        private WeaponSlot _weaponSlotRight;
        private WeaponSlot _weaponSlotLeft;
        private List<ArmorSlot> _armorSlots = new();

        public WeaponSlot WeaponSlotRight => _weaponSlotRight;
        public WeaponSlot WeaponSlotLeft => _weaponSlotLeft;
        public List<ArmorSlot> ArmorSlots => _armorSlots;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            WeaponSlot[] weaponSlots = GetComponentsInChildren<WeaponSlot>();
            foreach (WeaponSlot slot in weaponSlots)
            {
                if (slot.HandType == HandType.Right)
                {
                    _weaponSlotRight = slot;
                }
                else if (slot.HandType == HandType.Left)
                {
                    _weaponSlotLeft = slot;
                }
                slot.Initialize();
            }
            ArmorSlot[] armorSlots = GetComponentsInChildren<ArmorSlot>();
            foreach (ArmorSlot slot in armorSlots)
            {
                _armorSlots.Add(slot);
                slot.Initialize();
            }
        }

        public void EquipWeapon(WeaponItemConfig weapon, HandType handType, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (handType == HandType.Right)
            {
                EquipWeaponInRightHand(weapon, removeNewFromInventory, addOldToInventory);
            }
            else if (handType == HandType.Left)
            {
                EquipWeaponInLeftHand(weapon, removeNewFromInventory, addOldToInventory);
            }
        }

        public void EquipWeaponInRightHand(WeaponItemConfig weapon, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (weapon == null || _pawn.IsDead)
            {
                return;
            }
            if (removeNewFromInventory)
            {
                _pawn.PawnInventory.RemoveItem(weapon);
            }
            if (addOldToInventory)
            {
                _pawn.PawnInventory.AddItem(_weaponSlotRight.Config);
            }
            _weaponSlotRight.Setup(weapon);
            _pawn.PawnAnimator.PlayActionAnimation($"Swap Weapon Right", true);
            OnEquipmentChanged?.Invoke();
        }

        public void EquipWeaponInLeftHand(WeaponItemConfig weapon, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (weapon == null || _pawn.IsDead)
            {
                return;
            }
            if (removeNewFromInventory)
            {
                _pawn.PawnInventory.RemoveItem(weapon);
            }
            if (addOldToInventory)
            {
                _pawn.PawnInventory.AddItem(_weaponSlotLeft.Config);
            }
            _weaponSlotLeft.Setup(weapon);
            _pawn.PawnAnimator.PlayActionAnimation($"Swap Weapon Left", true);
            OnEquipmentChanged?.Invoke();
        }

        public void EquipArmor(ArmorItemConfig armor, bool removeFromInventory = true, bool addOldToInventory = true)
        {
            if (armor == null || _pawn.IsDead)
            {
                return;
            }
            if (removeFromInventory)
            {
                _pawn.PawnInventory.RemoveItem(armor);
            }
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (slot.Type == armor.ArmorType)
                {
                    if (addOldToInventory)
                    {
                        _pawn.PawnInventory.AddItem(slot.Config);
                    }
                    slot.Setup(armor);
                    break;
                }
            }
            OnEquipmentChanged?.Invoke();
        }

        public void EquipBestItems()
        {
            if (_pawn.PawnInventory.GetBestWeapon(out WeaponItemConfig weapon) && (_weaponSlotRight.Config == null || weapon.Rating > _weaponSlotRight.Config.Rating))
            {
                EquipWeaponInRightHand(weapon);
            }
            if (_pawn.PawnInventory.GetBestWeapon(out weapon) && (_weaponSlotLeft.Config == null || weapon.Rating > _weaponSlotLeft.Config.Rating))
            {
                EquipWeaponInLeftHand(weapon);
            }
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (_pawn.PawnInventory.GetBestArmor(slot.Type, out ArmorItemConfig armor) && (slot.Config == null || armor.Rating > slot.Config.Rating))
                {
                    EquipArmor(armor);
                }
            }
        }
    }
}