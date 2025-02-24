using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class DamageCollider : MonoBehaviour
    {
        public Action OnHitted;

        [SerializeField] private Collider _collider;
        [SerializeField] private List<DamageType> _damageTypes = new();
        [SerializeField] private List<EffectCreator> _targetEffects = new();
        [SerializeField] private float _splashRadius;

        private PawnController _owner;
        private List<EffectCreator> _ownerEffects = new();
        private Vector3 _hitPoint;
        private Vector3 _hitDirection;
        private List<PawnController> _damagedCharacters = new();

        public void Initialize(PawnController owner, List<DamageType> damageTypes, List<EffectCreator> ownerEffects, List<EffectCreator> targetEffects, float splashRadius)
        {
            _owner = owner;
            _damageTypes = new(damageTypes);
            _ownerEffects = new(ownerEffects);
            _targetEffects = new(targetEffects);
            _splashRadius = splashRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            PawnController target = other.GetComponentInParent<PawnController>();
            if (_splashRadius > 0f)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, _splashRadius, GameManager.StaticInstance.LayerManager.PawnMask);
                foreach (Collider collider in colliders)
                {
                    target = collider.GetComponentInParent<PawnController>();
                    if (CanHitTarget(target))
                    {
                        ApplyHitToTarget(target, other);
                    }
                }
            }
            else if (CanHitTarget(target))
            {
                ApplyHitToTarget(target, other);
            }
        }

        private bool CanHitTarget(PawnController target)
        {
            if (_owner != null && _owner == target)
            {
                return false;
            }
            return target != null && !target.IsDead && !_damagedCharacters.Contains(target);
        }

        private void ApplyHitToTarget(PawnController target, Collider collider)
        {
            _damagedCharacters.Add(target);
            _hitPoint = GetHitPoint(collider);
            _hitDirection = GetHitDirection(target);
            if (_owner != null)
            {
                ApplyEffectsToTarget(_owner, _ownerEffects);
            }
            ApplyDamageToTarget(target);
            ApplyEffectsToTarget(target, _targetEffects);
            OnHitted?.Invoke();
        }

        private Vector3 GetHitPoint(Collider other)
        {
            return other.ClosestPointOnBounds(transform.position);
        }

        private Vector3 GetHitDirection(PawnController target)
        {
            //if (_owner != null)
            //{
            //    return (target.transform.position - _owner.transform.position).normalized;
            //}
            return (_hitPoint - transform.position).normalized;
        }

        private void ApplyDamageToTarget(PawnController target)
        {
            if (_owner != null)
            {
                foreach (DamageType type in _damageTypes)
                {
                    InstantHealthReduceEffect effect = (InstantHealthReduceEffect)GameManager.StaticInstance.ConfigsManager.HealthReduceEffect.CreateEffect(target, _owner, _owner.PawnStats.GetStatByName(type.Element.DamageStat.DisplayName).CurrentValue * _owner.PawnStats.DamageDealt.CurrentValue / 100f * type.Damage, 0f);
                    effect.Initialize(type.Element, _hitPoint, _hitDirection);
                    target.PawnEffects.AddEffect(effect);
                }
            }
            else
            {
                foreach (DamageType type in _damageTypes)
                {
                    InstantHealthReduceEffect effect = (InstantHealthReduceEffect)GameManager.StaticInstance.ConfigsManager.HealthReduceEffect.CreateEffect(target, _owner, type.Damage, 0f);
                    effect.Initialize(type.Element, _hitPoint, _hitDirection);
                    target.PawnEffects.AddEffect(effect);
                }
            }
        }

        private void ApplyEffectsToTarget(PawnController target, List<EffectCreator> effects)
        {
            if (target.IsDead)
            {
                return;
            }
            foreach (EffectCreator creator in effects)
            {
                if (creator.Chance > UnityEngine.Random.value)
                {
                    if (creator.OverrideDefaultValues)
                    {
                        target.PawnEffects.AddEffect(creator.Effect.CreateEffect(target, _owner, creator.Value, creator.Duration));
                    }
                    else
                    {
                        target.PawnEffects.AddEffect(creator.Effect.CreateEffect(target, _owner, creator.Effect.Value, creator.Effect.Duration));
                    }
                }
            }
        }

        public void EnableDamageCollider()
        {
            _collider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            _collider.enabled = false;
            _damagedCharacters.Clear();
        }
    }
}