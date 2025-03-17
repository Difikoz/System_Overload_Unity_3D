using UnityEngine;

namespace WinterUniverse
{
    public class PawnLocomotion
    {
        private PawnController _pawn;
        private Vector3 _moveDirection;

        public PawnLocomotion(PawnController pawn)
        {
            _pawn = pawn;
        }

        public void OnUpdate(float deltaTime)
        {
            // move towards
        }
    }
}