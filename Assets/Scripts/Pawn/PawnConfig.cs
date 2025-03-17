using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Pawn", menuName = "Winter Universe/Data/New Pawn")]
    public class PawnConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private NameConfig _firstName;
        [SerializeField] private NameConfig _lastName;
        [SerializeField] private RaceConfig _race;
        [SerializeField] private GenderConfig _gender;
        [SerializeField] private VisualConfig _visual;
        [SerializeField] private VoiceConfig _voice;
        [SerializeField] private FactionConfig _faction;
        [SerializeField] private StatCreatorConfig _statCreator;
        [SerializeField] private StateCreatorConfig _stateCreator;
        [SerializeField] private List<ItemStack> _itemStacks = new();

        public string ID => _id;
        public NameConfig FirstName => _firstName;
        public NameConfig LastName => _lastName;
        public RaceConfig Race => _race;
        public GenderConfig Gender => _gender;
        public VisualConfig Visual => _visual;
        public VoiceConfig Voice => _voice;
        public FactionConfig Faction => _faction;
        public StatCreatorConfig StatCreator => _statCreator;
        public StateCreatorConfig StateCreator => _stateCreator;
        public List<ItemStack> ItemStacks => _itemStacks;

        public PawnData GetData()
        {
            PawnData data = new()
            {
                FirstName = _firstName.ID,
                LastName = _lastName.ID,
                Race = _race.ID,
                Gender = _gender.ID,
                Visual = _visual.ID,
                Voice = _voice.ID,
                Faction = _faction.ID,
                StatCreator = _statCreator.ID,
                StateCreator = _stateCreator.ID,
                Transform = new(),
                ItemStacks = new(),
            };
            foreach (ItemStack stack in _itemStacks)
            {
                if (data.ItemStacks.ContainsKey(stack.Item.ID))
                {
                    data.ItemStacks[stack.Item.ID] += stack.Amount;
                }
                else
                {
                    data.ItemStacks.Add(stack.Item.ID, stack.Amount);
                }
            }
            return data;
        }
    }
}