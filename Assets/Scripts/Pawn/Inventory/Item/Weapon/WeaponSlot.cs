//using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        //private PawnController _pawn;
        private WeaponItemConfig _config;
        private GameObject _model;

        public WeaponItemConfig Config => _config;

        public void Initialize()
        {
            //_pawn = GetComponentInParent<PawnController>();
        }

        public void ChangeConfig(WeaponItemConfig config)
        {
            if (_config != null)
            {
                // remove modifiers
            }
            if (_model != null)
            {
                //LeanPool.Despawn(_model);
                _model = null;
            }
            _config = config;
            if (_config != null)
            {
                // add modifiers
                //_model = LeanPool.Spawn(_config.Model, transform);
            }
        }
    }
}