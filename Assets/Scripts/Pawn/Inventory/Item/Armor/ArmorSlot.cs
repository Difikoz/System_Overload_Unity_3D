using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ArmorSlot : MonoBehaviour
    {
        [SerializeField] private List<ArmorRenderer> _armorRenderes = new();

        //private PawnController _pawn;
        private ArmorItemConfig _config;

        public ArmorItemConfig Config => _config;

        public void Initialize()
        {
            //_pawn = GetComponentInParent<PawnController>();
            ChangeConfig(null);
        }

        public void ChangeConfig(ArmorItemConfig config)
        {
            if (_config != null)
            {
                //_pawn.Status.RemoveStatModifiers(_config.Modifiers);
            }
            _config = config;
            if (_config != null)
            {
                //_pawn.Status.AddStatModifiers(_config.Modifiers);
            }
            foreach (ArmorRenderer ar in _armorRenderes)
            {
                ar.Toggle(ar.Config == _config);
            }
        }
    }
}