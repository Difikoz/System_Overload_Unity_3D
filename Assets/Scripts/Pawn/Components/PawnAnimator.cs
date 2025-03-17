using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;
        private CharacterController _cc;

        public void Initialize(PawnController pawn)
        {
            _pawn = pawn;
            _animator = GetComponent<Animator>();
            _cc = GetComponent<CharacterController>();
        }

        public void PlayAction(string name, float fadeTime = 0.1f, bool isPerfomingAction = true)
        {
            _pawn.StateHolder.SetStateValue("Is Perfoming Action", isPerfomingAction);
            _animator.CrossFade(name, fadeTime);
        }

        public void SetFloat(string name, float value)
        {
            _animator.SetFloat(name, value);
        }

        public void SetBool(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        public void Move(Vector3 direction, float deltaTime)
        {
            _cc.Move(direction * deltaTime);
        }

        public void RotateToDirection(Vector3 direction, float deltaTime)
        {
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), deltaTime);
            }
        }

        public bool GroundCast()
        {
            return Physics.SphereCast(transform.position + _cc.center, _cc.radius, Vector3.down, out _, _cc.center.y - _cc.radius * 0.75f, GameManager.StaticInstance.LayersManager.ObstacleMask);
        }
    }
}