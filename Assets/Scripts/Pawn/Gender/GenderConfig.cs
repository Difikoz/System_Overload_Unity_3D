using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Gender", menuName = "Winter Universe/Pawn/New Gender")]
    public class GenderConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _displayName;

        public string ID => _id;
        public string DisplayName => _displayName;
    }
}