using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "State Holder", menuName = "Winter Universe/Pawn/New State Holder")]
    public class StateHolderConfig : ScriptableObject
    {
        [SerializeField] private List<StateConfig> _states = new();

        public List<StateConfig> States => _states;
    }
}