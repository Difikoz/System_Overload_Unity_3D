using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class StatCreator
    {
        [SerializeField] private StatConfig _config;
        [SerializeField] private float _baseValue;

        public StatConfig Config => _config;
        public float BaseValue => _baseValue;
    }
}