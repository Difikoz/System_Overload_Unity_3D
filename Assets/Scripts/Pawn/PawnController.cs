using UnityEngine;

namespace WinterUniverse
{
    public class PawnController : MonoBehaviour
    {
        private EffectHolder _effectHolder;
        private Faction _faction;
        private Inventory _inventory;
        private StatHolder _statHolder;
        private StateHolder _stateHolder;

        public EffectHolder EffectHolder => _effectHolder;
        public Faction Faction => _faction;
        public Inventory Inventory => _inventory;
        public StatHolder StatHolder => _statHolder;
        public StateHolder StateHolder => _stateHolder;
    }
}