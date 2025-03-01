using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnInteraction : MonoBehaviour
    {
        public Action<Interactable> OnInteractableChanged;

        private PawnController _pawn;
        private Interactable _interactable;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public void AttempToInteract()
        {
            if (!_pawn.CanInteract || _interactable == null)
            {
                return;
            }
            if (_interactable.CanInteract(_pawn))
            {
                _interactable.Interact(_pawn);
            }
        }

        public void InvokeOnInteractableChanged()
        {
            OnInteractableChanged?.Invoke(_interactable);
        }
    }
}