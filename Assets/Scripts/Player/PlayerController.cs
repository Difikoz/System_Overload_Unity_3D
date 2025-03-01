using Lean.Pool;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class PlayerController : MonoBehaviour
    {
        private PawnController _pawn;
        private Vector2 _moveInput;
        private bool _runInput;
        private bool _jumpInput;
        private bool _interactInput;
        private bool _actionMainInput;
        private bool _actionSecondInput;
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

        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        public void OnRun(InputValue value)
        {
            _runInput = value.isPressed;
        }

        public void OnJump(InputValue value)
        {
            _jumpInput = value.isPressed;
        }

        public void OnInteract(InputValue value)
        {
            _interactInput = value.isPressed;
        }

        public void OnActionMainHand(InputValue value)
        {
            _actionMainInput = value.isPressed;
        }

        public void OnActionSecondHand(InputValue value)
        {
            _actionSecondInput = value.isPressed;
        }

        public void OnUpdate()
        {
            _pawn.MoveDirection = GameManager.StaticInstance.CameraManager.transform.right * _moveInput.x + GameManager.StaticInstance.CameraManager.transform.forward * _moveInput.y;
            if (Physics.Raycast(_pawn.PawnAnimator.HeadPoint.position, GameManager.StaticInstance.CameraManager.transform.forward, out _cameraHit, float.MaxValue, GameManager.StaticInstance.LayerManager.DetectableMask))
            {
                _pawn.LookDirection = (_cameraHit.point - _pawn.PawnAnimator.HeadPoint.position).normalized;
            }
            else
            {
                _pawn.LookDirection = GameManager.StaticInstance.CameraManager.transform.forward;
            }
            _pawn.IsRunning = _runInput && _pawn.PawnLocomotion.HandleRunning();
            _pawn.IsJumping = _jumpInput;
            _pawn.IsInteracting = _interactInput;
            _pawn.IsRightHandAttacking = _actionMainInput;
            if (_actionSecondInput)
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
                else
                {
                    data.Armors.Add($"Human {slot.Type.DisplayName}");// example - Human Body
                }
            }
            data.Transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.eulerAngles);
        }

        public void LoadData(PawnSaveData data)
        {
            _pawn.CreatePawn(data, "Player");
            _pawn.PawnStats.SetCurrentHealth(data.Health);
            _pawn.PawnStats.SetCurrentEnergy(data.Energy);
            _pawn.transform.SetPositionAndRotation(data.Transform.GetPosition(), data.Transform.GetRotation());
            GameManager.StaticInstance.CameraManager.transform.position = _pawn.transform.position;
        }
    }
}