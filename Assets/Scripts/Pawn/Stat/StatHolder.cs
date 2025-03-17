using System;
using System.Collections.Generic;

namespace WinterUniverse
{
    public class StatHolder
    {
        public Action OnStatsChanged;

        private List<Stat> _stats;
        private Stat _healthMax;
        private Stat _staminaMax;
        private Stat _healthRegeneration;
        private Stat _staminaRegeneration;
        private Stat _slicingResistance;
        private Stat _piercingResistance;
        private Stat _bluntResistance;
        private Stat _thermalResistance;
        private Stat _electricalResistance;
        private Stat _chemicalResistance;

        public List<Stat> Stats => _stats;
        public Stat HealthMax => _healthMax;
        public Stat StaminaMax => _staminaMax;
        public Stat HealthRegeneration => _healthRegeneration;
        public Stat StaminaRegeneration => _staminaRegeneration;
        public Stat SlicingResistance => _slicingResistance;
        public Stat PiercingResistance => _piercingResistance;
        public Stat BluntResistance => _bluntResistance;
        public Stat ThermalResistance => _thermalResistance;
        public Stat ElectricalResistance => _electricalResistance;
        public Stat ChemicalResistance => _chemicalResistance;

        public StatHolder(List<StatCreator> stats)
        {
            _stats = new();
            foreach (StatCreator stat in stats)
            {
                _stats.Add(new(stat.Config, stat.BaseValue));
            }
            AssignStats();
        }

        private void AssignStats()
        {
            foreach (Stat s in _stats)
            {
                switch (s.Config.ID)
                {
                    case "HP MAX":
                        _healthMax = s;
                        break;
                    case "SP MAX":
                        _staminaMax = s;
                        break;
                    case "HP REGEN":
                        _healthRegeneration = s;
                        break;
                    case "SP REGEN":
                        _staminaRegeneration = s;
                        break;
                    case "SLICING RES":
                        _slicingResistance = s;
                        break;
                    case "PIERCING RES":
                        _piercingResistance = s;
                        break;
                    case "BLUNT RES":
                        _bluntResistance = s;
                        break;
                    case "THERMAL RES":
                        _thermalResistance = s;
                        break;
                    case "ELECTRICAL RES":
                        _electricalResistance = s;
                        break;
                    case "CHEMICAL RES":
                        _chemicalResistance = s;
                        break;

                }
            }
        }

        public void RecalculateStats()
        {
            foreach (Stat s in _stats)
            {
                s.CalculateCurrentValue();
            }
            OnStatsChanged?.Invoke();
        }

        public Stat GetStat(string name)
        {
            foreach (Stat s in _stats)
            {
                if (s.Config.ID == name)
                {
                    return s;
                }
            }
            return null;
        }

        public void AddStatModifiers(List<StatModifierCreator> modifiers)
        {
            foreach (StatModifierCreator smc in modifiers)
            {
                AddStatModifier(smc);
            }
            RecalculateStats();
        }

        private void AddStatModifier(StatModifierCreator smc)
        {
            GetStat(smc.Config.ID).AddModifier(smc.Modifier);
        }

        public void RemoveStatModifiers(List<StatModifierCreator> modifiers)
        {
            foreach (StatModifierCreator smc in modifiers)
            {
                RemoveStatModifier(smc);
            }
            RecalculateStats();
        }

        private void RemoveStatModifier(StatModifierCreator smc)
        {
            GetStat(smc.Config.ID).RemoveModifier(smc.Modifier);
        }
    }
}