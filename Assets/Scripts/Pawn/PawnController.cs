using UnityEngine;

namespace WinterUniverse
{
    public class PawnController : MonoBehaviour
    {
        private PawnData _data;
        private EffectHolder _effectHolder;
        private FactionConfig _faction;
        private GenderConfig _gender;
        private Inventory _inventory;
        private NameConfig _firstName;
        private NameConfig _lastName;
        private RaceConfig _race;
        private StatHolder _statHolder;
        private StateHolder _stateHolder;
        private VisualConfig _visual;
        private VoiceConfig _voice;

        public PawnData Data => _data;
        public EffectHolder EffectHolder => _effectHolder;
        public FactionConfig Faction => _faction;
        public GenderConfig Gender => _gender;
        public Inventory Inventory => _inventory;
        public RaceConfig Race => _race;
        public NameConfig FirstName => _firstName;
        public NameConfig LastName => _lastName;
        public StatHolder StatHolder => _statHolder;
        public StateHolder StateHolder => _stateHolder;
        public VisualConfig Visual => _visual;
        public VoiceConfig Voice => _voice;

        public void NewData(PawnData data)
        {
            _data = data;
            // assign first name
            // assign last name
            // assign icon
            // spawn visual
            //_visual=;
            // assign race
            //_race=;
            // assign gender
            //_gender = ;
            // assign voice
            //_voice=;
            // change faction
            // init stat holder
            // init state holder
            // set transform
            // add items
        }

        public void SaveData(ref PawnData data)
        {
            data.FirstName = _data.FirstName;
            data.LastName = _data.LastName;
            data.Icon = _data.Icon;
            data.Race = _race.ID;
            data.Gender = _gender.ID;
            data.Visual = _visual.ID;
            data.Voice = _voice.ID;
            data.Faction = _faction.ID;
            data.Transform.SetPositionAndRotation(transform.position, transform.eulerAngles);
            data.ItemStacks = new();
            foreach (ItemStack stack in _inventory.Stacks)
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
            data.Health = 1f;
            data.Stamina = 1f;
        }

        public void LoadData(PawnData data)
        {
            NewData(data);
            // set items ?
            // set weapon
            // set armor
            // set transform
            // set health
            // set stamina
            // set transform
        }
    }
}