using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Winter Universe/Item/New Armor")]
    public class ArmorItemConfig : ItemConfig
    {
        [SerializeField] private List<StatModifierCreator> _modifiers = new();
        //[SerializeField] private List<EffectCreator> _effects = new();

        public List<StatModifierCreator> Modifiers => _modifiers;
        //public List<EffectCreator> Effects => _effects;

        private void OnValidate()
        {
            _itemType = ItemType.Armor;
        }
    }
}