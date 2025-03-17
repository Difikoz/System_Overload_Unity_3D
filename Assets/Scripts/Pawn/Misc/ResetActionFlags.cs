using UnityEngine;

namespace WinterUniverse
{
    public class ResetActionFlags : StateMachineBehaviour
    {
        private PawnController _pawn;

        [SerializeField] private bool _toggleIsPerfomingAction = true;
        [SerializeField] private bool _isPerfomingActionState = false;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _pawn = animator.GetComponent<PawnController>();
            if (_toggleIsPerfomingAction)
            {
                _pawn.StateHolder.SetStateValue("Is Perfoming Action", _isPerfomingActionState);
            }
        }
    }
}