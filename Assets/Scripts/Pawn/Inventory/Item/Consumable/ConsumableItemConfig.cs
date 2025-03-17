using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "Winter Universe/Item/New Consumable")]
    public class ConsumableItemConfig : ItemConfig
    {
        //[SerializeField] private List<EffectCreator> _effects = new();

        //public List<EffectCreator> Effects => _effects;

        private void OnValidate()
        {
            _itemType = ItemType.Consumable;
        }
    }
}