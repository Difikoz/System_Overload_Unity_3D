using UnityEngine;

namespace WinterUniverse
{
    public class PawnLocomotion
    {
        private PawnController _pawn;
        private Vector3 _moveVelocity;
        private Vector3 _fallVelocity;
        private bool _isMoving;
        private bool _isGrounded;

        public Vector3 MoveDirection;
        public Vector3 LookDirection;

        public PawnLocomotion(PawnController pawn)
        {
            _pawn = pawn;
        }

        public void OnUpdate(float deltaTime)
        {
            _isGrounded = _fallVelocity.y <= 0f && _pawn.Animator.GroundCast();
            if (_isGrounded)
            {
                _fallVelocity.y = GameManager.StaticInstance.ConfigsManager.Gravity;
            }
            else
            {
                _fallVelocity.y += GameManager.StaticInstance.ConfigsManager.Gravity * deltaTime;
            }
            if (MoveDirection != Vector3.zero && _pawn.StateHolder.CompareStateValue("Is Perfoming Action", false))
            {
                if (_pawn.StateHolder.CompareStateValue("Is Running", true))
                {
                    _moveVelocity = Vector3.MoveTowards(_moveVelocity, MoveDirection * 2f, 4f * deltaTime);
                    _pawn.Animator.RotateToDirection(LookDirection, 2f * deltaTime);
                }
                else
                {
                    _moveVelocity = Vector3.MoveTowards(_moveVelocity, MoveDirection, 2f * deltaTime);
                    _pawn.Animator.RotateToDirection(LookDirection, 4f * deltaTime);
                }
            }
            else
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, Vector3.zero, 4f * deltaTime);
            }
            _isMoving = _moveVelocity.magnitude > 0f;
            _pawn.Animator.SetFloat("Forward Velocity", Vector3.Dot(_moveVelocity, _pawn.Animator.transform.forward));
            _pawn.Animator.SetFloat("Right Velocity", Vector3.Dot(_moveVelocity, _pawn.Animator.transform.right));
            _pawn.Animator.SetFloat("Fall Velocity", -1f);
            _pawn.Animator.SetFloat("Turn Velocity", Vector3.SignedAngle(_pawn.Animator.transform.forward, LookDirection, _pawn.Animator.transform.up) / 45f);
            _pawn.Animator.SetBool("Is Moving", _isMoving);
            _pawn.Animator.SetBool("Is Grounded", _isGrounded);
            _pawn.StateHolder.SetStateValue("Is Moving", _isMoving);
            _pawn.StateHolder.SetStateValue("Is Grounded", _isGrounded);
            _pawn.Animator.Move(_fallVelocity, deltaTime);
        }
    }
}