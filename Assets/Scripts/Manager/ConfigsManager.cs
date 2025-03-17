using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ConfigsManager : MonoBehaviour
    {
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private List<FactionConfig> _factions = new();
        [SerializeField] private List<GenderConfig> _genders = new();
        [SerializeField] private List<InventoryConfig> _inventory = new();
        [SerializeField] private List<NameConfig> _names = new();
        [SerializeField] private List<RaceConfig> _races = new();
        [SerializeField] private List<StatConfig> _stats = new();
        [SerializeField] private List<StatCreatorConfig> _statCreators = new();
        [SerializeField] private List<StateConfig> _states = new();
        [SerializeField] private List<StateCreatorConfig> _stateCreators = new();
        [SerializeField] private List<VisualConfig> _visuals = new();
        [SerializeField] private List<VoiceConfig> _voices = new();
        [SerializeField] private List<WeaponItemConfig> _weapons = new();
        [SerializeField] private List<ArmorItemConfig> _armors = new();
        [SerializeField] private List<ConsumableItemConfig> _consumables = new();
        [SerializeField] private List<ResourceItemConfig> _resources = new();

        private List<ItemConfig> _items;

        public float Gravity => _gravity;
        public List<StatConfig> Stats => _stats;
        public List<StateConfig> States => _states;

        public void InitializeComponent()
        {
            _items = new();
            foreach (WeaponItemConfig config in _weapons)
            {
                _items.Add(config);
            }
            foreach (ArmorItemConfig config in _armors)
            {
                _items.Add(config);
            }
            foreach (ConsumableItemConfig config in _consumables)
            {
                _items.Add(config);
            }
            foreach (ResourceItemConfig config in _resources)
            {
                _items.Add(config);
            }
        }

        public FactionConfig GetFaction(string id)
        {
            foreach (FactionConfig config in _factions)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public GenderConfig GetGender(string id)
        {
            foreach (GenderConfig config in _genders)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public InventoryConfig GetInventory(string id)
        {
            foreach (InventoryConfig config in _inventory)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public NameConfig GetName(string id)
        {
            foreach (NameConfig config in _names)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public RaceConfig GetRace(string id)
        {
            foreach (RaceConfig config in _races)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public StatCreatorConfig GetStatCreator(string id)
        {
            foreach (StatCreatorConfig config in _statCreators)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public StateCreatorConfig GetStateCreator(string id)
        {
            foreach (StateCreatorConfig config in _stateCreators)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public VisualConfig GetVisual(string id)
        {
            foreach (VisualConfig config in _visuals)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public VoiceConfig GetVoice(string id)
        {
            foreach (VoiceConfig config in _voices)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public ItemConfig GetItem(string id)
        {
            foreach (ItemConfig config in _items)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public WeaponItemConfig GetWeapon(string id)
        {
            foreach (WeaponItemConfig config in _weapons)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public ArmorItemConfig GetArmor(string id)
        {
            foreach (ArmorItemConfig config in _armors)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public ConsumableItemConfig GetConsumable(string id)
        {
            foreach (ConsumableItemConfig config in _consumables)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }

        public ResourceItemConfig GetResource(string id)
        {
            foreach (ResourceItemConfig config in _resources)
            {
                if (config.ID == id)
                {
                    return config;
                }
            }
            return null;
        }
    }
}