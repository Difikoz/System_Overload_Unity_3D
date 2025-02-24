using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class WorldPlayerManager : MonoBehaviour
    {
        private PawnController _pawn;
        private RaycastHit _cameraHit;

        public PawnController Pawn => _pawn;

        public void Initialize()
        {
            _pawn = LeanPool.Spawn(GameManager.StaticInstance.ObjectManager.HumanPrefab).GetComponent<PawnController>();
            _pawn.Initialize();
            _pawn.OnDied += OnDeath;
            _pawn.OnRevived += OnRevive;
        }

        private void OnDestroy()
        {
            _pawn.OnDied -= OnDeath;
            _pawn.OnRevived -= OnRevive;
        }

        public void OnUpdate()
        {
            _pawn.MoveDirection = GameManager.StaticInstance.CameraManager.transform.right * GameManager.StaticInstance.InputManager.MoveInput.x + GameManager.StaticInstance.CameraManager.transform.forward * GameManager.StaticInstance.InputManager.MoveInput.y;
            if (Physics.Raycast(_pawn.PawnAnimator.HeadPoint.position, GameManager.StaticInstance.CameraManager.transform.forward, out _cameraHit, float.MaxValue, GameManager.StaticInstance.LayerManager.DetectableMask))
            {
                _pawn.LookDirection = (_cameraHit.point - _pawn.PawnAnimator.HeadPoint.position).normalized;
            }
            else
            {
                _pawn.LookDirection = GameManager.StaticInstance.CameraManager.transform.forward;
            }
            _pawn.IsRunning = GameManager.StaticInstance.InputManager.RunInput && _pawn.PawnLocomotion.HandleRunning();
            _pawn.IsJumping = GameManager.StaticInstance.InputManager.JumpInput;
            _pawn.IsInteracting = GameManager.StaticInstance.InputManager.InteractInput;
            _pawn.IsRightHandAttacking = GameManager.StaticInstance.InputManager.ActionMainInput;
            if (GameManager.StaticInstance.InputManager.ActionSecondInput)
            {
                _pawn.IsLeftHandAttacking = true;
                //zoom in camera
            }
            else
            {
                _pawn.IsLeftHandAttacking = false;
                //zoom out camera
            }
            //transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.rotation);
            _pawn.OnUpdate();
        }

        private void OnDeath()
        {
            GameManager.StaticInstance.UIManager.NotificationUI.DisplayNotification("You Died");
        }

        private void OnRevive()
        {
            _pawn.transform.SetPositionAndRotation(GameManager.StaticInstance.SaveLoadManager.CurrentSaveData.RespawnTransform.GetPosition(), GameManager.StaticInstance.SaveLoadManager.CurrentSaveData.RespawnTransform.GetRotation());
        }

        public void SaveData(ref PawnSaveData data)
        {
            data.PawnName = _pawn.CharacterName;
            data.Faction = _pawn.Faction.DisplayName;
            data.Health = _pawn.PawnStats.HealthCurrent;
            data.Energy = _pawn.PawnStats.EnergyCurrent;
            data.InventoryStacks.Clear();
            foreach (ItemStack stack in _pawn.PawnInventory.Stacks)
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
            if (_pawn.PawnEquipment.WeaponSlotRight.Config != null)
            {
                data.WeaponInRightHand = _pawn.PawnEquipment.WeaponSlotRight.Config.DisplayName;
            }
            else
            {
                data.WeaponInRightHand = "empty";
            }
            if (_pawn.PawnEquipment.WeaponSlotLeft.Config != null)
            {
                data.WeaponInLeftHand = _pawn.PawnEquipment.WeaponSlotLeft.Config.DisplayName;
            }
            else
            {
                data.WeaponInLeftHand = "empty";
            }
            data.Armors.Clear();
            foreach (ArmorSlot slot in _pawn.PawnEquipment.ArmorSlots)
            {
                if (slot.Config != null)
                {
                    data.Armors.Add(slot.Config.DisplayName);
                }
            }
            data.Transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.eulerAngles);
        }

        public void LoadData(PawnSaveData data)
        {
            _pawn.CreatePawn(data);
            _pawn.PawnStats.SetCurrentHealth(data.Health);
            _pawn.PawnStats.SetCurrentEnergy(data.Energy);
            _pawn.transform.SetPositionAndRotation(data.Transform.GetPosition(), data.Transform.GetRotation());
            GameManager.StaticInstance.CameraManager.transform.position = _pawn.transform.position;
        }
    }
}