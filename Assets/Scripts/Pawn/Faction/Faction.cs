using System;

namespace WinterUniverse
{
    public class Faction
    {
        public Action OnFactionChanged;

        private PawnController _pawn;
        private FactionConfig _config;

        public FactionConfig Config => _config;

        public Faction(PawnController pawn)
        {
            _pawn = pawn;
        }

        public void ChangeConfig(FactionConfig config)
        {
            _config = config;
            OnFactionChanged?.Invoke();
        }
    }
}