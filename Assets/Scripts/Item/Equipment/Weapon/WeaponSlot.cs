using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        private PawnController _pawn;
        private WeaponItemConfig _config;

        [SerializeField] private HandType _handType;

        public PawnController Pawn => _pawn;
        public WeaponItemConfig Config => _config;
        public HandType HandType => _handType;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
        }

        public void Setup(WeaponItemConfig weapon)
        {
            if (_config != null)
            {
                foreach (StatModifierCreator creator in _config.Modifiers)
                {
                    _pawn.PawnStats.RemoveStatModifier(creator);
                }
                // remove model
            }
            _config = weapon;
            foreach (StatModifierCreator creator in _config.Modifiers)
            {
                _pawn.PawnStats.AddStatModifier(creator);
            }
            // attach model
            //_shootPoint = _model.GetComponentInChildren<ShootPoint>();
        }

        public void PerformAction()
        {
            // check for energy
            // consume energy
            // spawn projectile
        }
    }
}