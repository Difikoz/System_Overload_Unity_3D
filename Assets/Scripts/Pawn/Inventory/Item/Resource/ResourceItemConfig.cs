using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Resource", menuName = "Winter Universe/Item/New Resource")]
    public class ResourceItemConfig : ItemConfig
    {
        private void OnValidate()
        {
            _itemType = ItemType.Resource;
        }
    }
}