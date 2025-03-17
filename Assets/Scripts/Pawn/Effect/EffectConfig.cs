using UnityEngine;

namespace WinterUniverse
{
    public abstract class EffectConfig : BasicInfoConfig
    {
        [SerializeField] protected EffectType _effectType;

        public EffectType EffectType => _effectType;

        public abstract Effect CreateEffect(PawnController target, PawnController source, float value, float duration);
    }
}