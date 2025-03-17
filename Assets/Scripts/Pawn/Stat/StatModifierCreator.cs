using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class StatModifierCreator
    {
        [SerializeField] private StatConfig _config;
        [SerializeField] private StatModifier _modifier;

        public StatConfig Config => _config;
        public StatModifier Modifier => _modifier;

        public StatModifierCreator(StatConfig config, StatModifier modifier)
        {
            _config = config;
            _modifier = modifier;
        }
    }
}