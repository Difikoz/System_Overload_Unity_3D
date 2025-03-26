using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(CharacterController))]
    public class PawnLocomotion : PawnComponent
    {
        [SerializeField] private float _rotateSpeed = 180f;
        [SerializeField] private float _turnAngle = 45f;
        [SerializeField] private LayerMask _obstacleMask;

        private RaycastHit _groundHit;

        public Vector3 MoveDirection;
        public Vector3 LookDirection;
        public CharacterController CC { get; private set; }
        public Vector3 GroundVelocity { get; private set; }
        public Vector3 FallVelocity { get; private set; }
        public Vector3 Gravity { get; private set; }
        public MovementState MovementState { get; private set; }
        public float ForwardVelocity { get; private set; }
        public float RightVelocity { get; private set; }
        public float TurnVelocity { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            CC = GetComponent<CharacterController>();
            Gravity = new(0f, 10f, 0f);// get from manager
        }

        public override void OnUpdate()
        {
            CheckGround();
            CalculateFallVelocity();
            CalculateGroundVelocity();
            HandleRotation();
            CC.Move(FallVelocity * Time.deltaTime);
        }

        protected virtual void CheckGround()
        {
            if (MovementState != MovementState.InAir)
            {
                if (Vector3.Dot(transform.up, FallVelocity) > 0f || !StayOnGround())
                {
                    GroundVelocity = Vector3.zero;
                    MovementState = MovementState.InAir;
                }
            }
            else if (MovementState == MovementState.InAir)
            {
                if (Vector3.Dot(transform.up, FallVelocity) <= 0f && StayOnGround())
                {
                    FallVelocity = -Gravity;
                    MovementState = MovementState.Idle;
                }
            }
        }

        protected virtual void CalculateFallVelocity()
        {
            if (MovementState == MovementState.InAir)
            {
                FallVelocity -= Gravity * Time.deltaTime;
            }
        }

        protected virtual void CalculateGroundVelocity()
        {
            if (MovementState == MovementState.InAir || MoveDirection == Vector3.zero)
            {
                GroundVelocity = Vector3.MoveTowards(GroundVelocity, Vector3.zero, 4f * Time.deltaTime);
                if (GroundVelocity == Vector3.zero && (MovementState == MovementState.Walk || MovementState == MovementState.Run))
                {
                    MovementState = MovementState.Idle;
                }
            }
            else if (MovementState == MovementState.Idle)
            {
                MovementState = MovementState.Walk;
            }
            else if (MovementState == MovementState.Walk)
            {
                GroundVelocity = Vector3.MoveTowards(GroundVelocity, MoveDirection, 2f * Time.deltaTime);
            }
            else if (MovementState == MovementState.Run)
            {
                GroundVelocity = Vector3.MoveTowards(GroundVelocity, MoveDirection * 2f, 4f * Time.deltaTime);
            }
            ForwardVelocity = Vector3.Dot(transform.forward, GroundVelocity);
            RightVelocity = Vector3.Dot(transform.right, GroundVelocity);
        }

        protected virtual void HandleRotation()
        {
            TurnVelocity = Mathf.MoveTowards(TurnVelocity, Vector3.SignedAngle(transform.forward, LookDirection, transform.up) / _turnAngle, 2f * Time.deltaTime);
            if (MovementState == MovementState.Idle || MovementState == MovementState.InAir || LookDirection == Vector3.zero)
            {
                return;
            }
            transform.Rotate(transform.up * TurnVelocity * _rotateSpeed * Time.deltaTime);
        }

        protected virtual bool StayOnGround()
        {
            return Physics.SphereCast(transform.position + transform.up * CC.center.y, CC.radius, -transform.up, out _groundHit, CC.center.y - CC.radius * 0.8f, _obstacleMask);
        }

        public void ToggleSprint()
        {
            if (MovementState == MovementState.Walk)
            {
                MovementState = MovementState.Run;
            }
            else if (MovementState == MovementState.Run)
            {
                MovementState = MovementState.Walk;
            }
        }

        public void StartSprint()
        {
            if (MovementState == MovementState.Walk)
            {
                MovementState = MovementState.Run;
            }
        }

        public void StopSprint()
        {
            if (MovementState == MovementState.Run)
            {
                MovementState = MovementState.Walk;
            }
        }
    }
}