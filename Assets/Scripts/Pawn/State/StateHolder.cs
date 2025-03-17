using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class StateHolder
    {
        public Action OnStatesChanged;

        private Dictionary<string, bool> _states;

        public Dictionary<string, bool> States => _states;

        public StateHolder(StateCreatorConfig config)
        {
            _states = new();
            foreach (StateConfig state in GameManager.StaticInstance.ConfigsManager.States)
            {
                SetStateValue(state.Key, false);
            }
            foreach (StateCreator state in config.States)
            {
                SetStateValue(state.Config.Key, state.Value);
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
            OnStatesChanged?.Invoke();
        }
    }
}