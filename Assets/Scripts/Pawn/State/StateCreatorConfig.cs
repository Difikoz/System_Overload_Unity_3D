using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "State Creator", menuName = "Winter Universe/Pawn/New State Creator")]
    public class StateCreatorConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private List<StateCreator> _states = new();

        public string ID => _id;
        public List<StateCreator> States => _states;
    }
}