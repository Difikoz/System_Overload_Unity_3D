using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class StateHolder
    {
        private Dictionary<string, bool> _states;

        public Dictionary<string, bool> States => _states;

        public StateHolder(List<StateConfig> states)
        {
            _states = new();
            foreach (StateConfig state in states)
            {
                SetStateValue(state.Key, false);
            }
        }

        public bool HasState(string key)
        {
            return _states.ContainsKey(key);
        }

        public bool CompareStateValue(string key, bool value)
        {
            if (_states.ContainsKey(key))
            {
                return _states[key] == value;
            }
            Debug.Log($"not have {key} state to compare");
            return false;
        }

        public void SetStateValue(string key, bool value)
        {
            if (_states.ContainsKey(key))
            {
                _states[key] = value;
            }
            else
            {
                _states.Add(key, value);
            }
        }
    }
}