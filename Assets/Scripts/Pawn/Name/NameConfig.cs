using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Name", menuName = "Winter Universe/Pawn/New Name")]
    public class NameConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _displayName;

        public string ID => _id;
        public string DisplayName => _displayName;
    }
}