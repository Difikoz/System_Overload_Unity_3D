using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class Projectile : MonoBehaviour
    {
        public DamageCollider ProjectileCollider;
        public int PierceCount;

        private int _currentPierceCount;

        public void Shoot(PawnController owner, WeaponItemConfig weapon)
        {
            _currentPierceCount = 0;
            ProjectileCollider.Initialize(owner, weapon.DamageTypes, weapon.OwnerEffects, weapon.TargetEffects, weapon.SplashRadius);
            ProjectileCollider.OnHitted += OnHitted;
            ProjectileCollider.EnableDamageCollider();
        }

        private void OnHitted()
        {
            if (_currentPierceCount == PierceCount)
            {
                ProjectileCollider.OnHitted -= OnHitted;
                ProjectileCollider.DisableDamageCollider();
                LeanPool.Despawn(gameObject);
            }
            else
            {
                _currentPierceCount++;
            }
        }
    }
}