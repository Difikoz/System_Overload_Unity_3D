using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "State", menuName = "Winter Universe/Pawn/New State")]
    public class StateConfig : ScriptableObject
    {
        [SerializeField] private string _key;

        public string Key => _key;
    }
}