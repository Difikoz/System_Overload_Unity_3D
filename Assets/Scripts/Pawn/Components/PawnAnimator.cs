using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Animator))]
    public class PawnAnimator : PawnComponent
    {
        [SerializeField] private Transform _eyes;

        public Animator Animator { get; private set; }

        public Transform Eyes => _eyes;

        public override void Initialize()
        {
            base.Initialize();
            Animator = GetComponent<Animator>();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Animator.SetFloat("Ground Velocity", _pawn.Locomotion.GroundVelocity.magnitude);
            Animator.SetFloat("Forward Velocity", _pawn.Locomotion.ForwardVelocity);
            Animator.SetFloat("Right Velocity", _pawn.Locomotion.RightVelocity);
            Animator.SetFloat("Fall Velocity", _pawn.Locomotion.FallVelocity.y);
            Animator.SetFloat("Turn Velocity", _pawn.Locomotion.TurnVelocity);
            Animator.SetBool("Is Grounded", _pawn.Locomotion.MovementState != MovementState.InAir);
        }

        public virtual void PlayerAction(string name, float fadeTime = 0.1f, bool isPerfomingAction = true)
        {
            Animator.CrossFade(name, fadeTime);
        }
    }
}