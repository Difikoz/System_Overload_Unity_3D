using UnityEngine;

namespace WinterUniverse
{
    public abstract class PawnComponent : BasicComponent
    {
        protected PawnController _pawn;

        public override void Initialize()
        {
            base.Initialize();
            _pawn = GetComponent<PawnController>();
        }
    }
}