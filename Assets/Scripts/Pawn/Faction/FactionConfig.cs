using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Faction", menuName = "Winter Universe/Pawn/New Faction")]
    public class FactionConfig : BasicInfoConfig
    {
        [SerializeField] private List<FactionRelationship> _relationships = new();

        public List<FactionRelationship> Relationships => _relationships;

        public RelationshipState GetState(FactionConfig other)
        {
            foreach (FactionRelationship relationship in _relationships)
            {
                if (relationship.Faction == other)
                {
                    return relationship.State;
                }
            }
            Debug.LogError($"[{_displayName}] dont have relation with [{other.DisplayName}]. Fix it! Just now returned as [Neutral]!");
            return RelationshipState.Neutral;
        }
    }
}