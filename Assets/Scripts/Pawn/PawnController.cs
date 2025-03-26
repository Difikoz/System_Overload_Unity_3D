using UnityEngine;

namespace WinterUniverse
{
    public abstract class PawnController : BasicComponent
    {
        public PawnAnimator Animator { get; private set; }
        public PawnLocomotion Locomotion { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            Animator = GetComponent<PawnAnimator>();
            Locomotion = GetComponent<PawnLocomotion>();
            Animator.Initialize();
            Locomotion.Initialize();
        }

        public override void Destroy()
        {
            base.Destroy();
            Animator.Destroy();
            Locomotion.Destroy();
        }

        public override void Enable()
        {
            base.Enable();
            Animator.Enable();
            Locomotion.Enable();
        }

        public override void Disable()
        {
            base.Disable();
            Animator.Disable();
            Locomotion.Disable();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Animator.OnUpdate();
            Locomotion.OnUpdate();
        }
    }
}