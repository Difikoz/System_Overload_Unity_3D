using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Stat Creator", menuName = "Winter Universe/Pawn/New Stat Creator")]
    public class StatCreatorConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private List<StatCreator> _stats = new();

        public string ID => _id;
        public List<StatCreator> Stats => _stats;
    }
}