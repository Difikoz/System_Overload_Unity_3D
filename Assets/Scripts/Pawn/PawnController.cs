using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PawnAnimator))]
    [RequireComponent(typeof(PawnCombat))]
    [RequireComponent(typeof(PawnEffects))]
    [RequireComponent(typeof(PawnEquipment))]
    [RequireComponent(typeof(PawnInteraction))]
    [RequireComponent(typeof(PawnInventory))]
    [RequireComponent(typeof(PawnLocomotion))]
    [RequireComponent(typeof(PawnSound))]
    [RequireComponent(typeof(PawnStats))]
    public class PawnController : MonoBehaviour
    {
        public Action<FactionConfig> OnFactionChanged;
        public Action OnDied;
        public Action OnRevived;

        protected string _pawnName;
        protected FactionConfig _faction;
        protected PawnAnimator _pawnAnimator;
        protected PawnCombat _pawnCombat;
        protected PawnEffects _pawnEffects;
        protected PawnEquipment _pawnEquipment;
        protected PawnInteraction _pawnInteraction;
        protected PawnInventory _pawnInventory;
        protected PawnLocomotion _pawnLocomotion;
        protected PawnSound _pawnSound;
        protected PawnStats _pawnStats;

        public string CharacterName => _pawnName;
        public FactionConfig Faction => _faction;
        public PawnAnimator PawnAnimator => _pawnAnimator;
        public PawnCombat PawnCombat => _pawnCombat;
        public PawnEffects PawnEffects => _pawnEffects;
        public PawnEquipment PawnEquipment => _pawnEquipment;
        public PawnInventory PawnInventory => _pawnInventory;
        public PawnLocomotion PawnLocomotion => _pawnLocomotion;
        public PawnSound PawnSound => _pawnSound;
        public PawnStats PawnStats => _pawnStats;
        public PawnInteraction PawnInteraction => _pawnInteraction;

        public bool Created;
        public Vector3 MoveDirection;
        public Vector3 LookDirection;
        public float ForwardVelocity;
        public float RightVelocity;
        public float FallVelocity;
        public float TurnVelocity;
        public bool IsPerfomingAnimationAction;
        public bool UseGravity = true;
        public bool CanMove = true;
        public bool CanRotate = true;
        public bool CanJump = true;
        public bool CanDash = true;
        public bool CanAttack = true;
        public bool CanInteract = true;
        public bool IsGrounded = true;
        public bool IsMoving;
        public bool IsRunning;
        public bool IsJumping;
        public bool IsInteracting;
        public bool IsRightHandAttacking;
        public bool IsLeftHandAttacking;
        public bool IsInvulnerable;
        public bool IsDead;

        public void Initialize()
        {
            _pawnAnimator = GetComponent<PawnAnimator>();
            _pawnCombat = GetComponent<PawnCombat>();
            _pawnEffects = GetComponent<PawnEffects>();
            _pawnEquipment = GetComponent<PawnEquipment>();
            _pawnInteraction = GetComponent<PawnInteraction>();
            _pawnInventory = GetComponent<PawnInventory>();
            _pawnLocomotion = GetComponent<PawnLocomotion>();
            _pawnSound = GetComponent<PawnSound>();
            _pawnStats = GetComponent<PawnStats>();
            _pawnAnimator.Initialize();
            _pawnCombat.Initialize();
            _pawnEffects.Initialize();
            _pawnStats.Initialize();
            _pawnStats.CreateStats();
            _pawnEquipment.Initialize();
            _pawnInteraction.Initialize();
            _pawnLocomotion.Initialize();
            _pawnSound.Initialize();
        }

        public void OnUpdate()
        {
            if (!Created)
            {
                return;
            }
            _pawnEffects.TickEffects(Time.deltaTime);
            if (!IsDead)
            {
                _pawnStats.OnUpdate();
                _pawnCombat.OnUpdate();
                if (IsJumping)
                {
                    _pawnLocomotion.AttempToJump();
                }
                else if (IsInteracting)
                {
                    _pawnInteraction.AttempToInteract();
                }
                else if (IsRightHandAttacking)
                {
                    _pawnEquipment.WeaponSlotRight.PerformAction();
                }
                else if (IsRightHandAttacking)
                {
                    _pawnEquipment.WeaponSlotRight.PerformAction();
                }
            }
            _pawnAnimator.OnUpdate();
            _pawnLocomotion.OnUpdate();
        }

        public void CreatePawn(PawnSaveData data, string tag)
        {
            Created = false;
            _pawnName = data.PawnName;
            gameObject.tag = tag;
            _pawnInventory.Initialize(data.InventoryStacks);
            if (data.WeaponInRightHand != "empty")
            {
                _pawnEquipment.EquipWeaponInRightHand(GameManager.StaticInstance.ConfigsManager.GetWeapon(data.WeaponInRightHand), false, false);
            }
            if (data.WeaponInLeftHand != "empty")
            {
                _pawnEquipment.EquipWeaponInLeftHand(GameManager.StaticInstance.ConfigsManager.GetWeapon(data.WeaponInLeftHand), false, false);
            }
            foreach (string armor in data.Armors)
            {
                _pawnEquipment.EquipArmor(GameManager.StaticInstance.ConfigsManager.GetArmor(armor), false, false);
            }
            _pawnStats.RecalculateStats();
            ChangeFaction(GameManager.StaticInstance.ConfigsManager.GetFaction(data.Faction));
            IgnoreMyOwnColliders();// this order???
            Revive();
            Created = true;
        }

        public void DeletePawn()
        {
            Created = false;
        }

        public void ChangeFaction(FactionConfig data)
        {
            _faction = data;
            OnFactionChanged?.Invoke(_faction);
        }

        public void Die(PawnController source = null, string animationName = "Death")
        {
            if (!IsDead)
            {
                _pawnStats.HealthCurrent = 0f;
                _pawnStats.OnHealthChanged?.Invoke(0f, _pawnStats.HealthMax.CurrentValue);
                IsDead = true;
                _pawnAnimator.PlayActionAnimation(animationName, true);
                if (source != null)
                {

                }
                _pawnSound.PlayDeathClip();
                _pawnCombat.ResetTarget();
                OnDied?.Invoke();
                StartCoroutine(ProcessDeathEvent());
            }
        }

        private IEnumerator ProcessDeathEvent()
        {
            yield return new WaitForSeconds(5f);
            //LeanPool.Despawn(gameObject);
        }

        public void Revive()
        {
            if (IsDead)
            {
                _pawnAnimator.PlayActionAnimation("Revive", true);
                OnRevived?.Invoke();
            }
            IsDead = false;
            _pawnStats.RestoreCurrentHealth(_pawnStats.HealthMax.CurrentValue);
            _pawnStats.RestoreCurrentEnergy(_pawnStats.EnergyMax.CurrentValue);
        }

        private void IgnoreMyOwnColliders()
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();
            List<Collider> ignoreColliders = new();
            foreach (Collider c in colliders)
            {
                ignoreColliders.Add(c);
            }
            ignoreColliders.Add(GetComponent<Collider>());
            foreach (Collider c in ignoreColliders)
            {
                foreach (Collider other in ignoreColliders)
                {
                    Physics.IgnoreCollision(c, other, true);
                }
            }
        }
    }
}