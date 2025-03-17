using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnController
    {
        private bool _created;
        private PawnData _data;
        private PawnAnimator _animator;
        private PawnLocomotion _locomotion;
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
        public PawnAnimator Animator => _animator;
        public PawnLocomotion Locomotion => _locomotion;
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
            if (_created)
            {
                // delete created
                if (_animator != null)
                {
                    LeanPool.Despawn(_animator.gameObject);
                }
                _created = false;
            }
            _data = data;
            _firstName = GameManager.StaticInstance.ConfigsManager.GetName(data.FirstName);
            _lastName = GameManager.StaticInstance.ConfigsManager.GetName(data.LastName);
            _race = GameManager.StaticInstance.ConfigsManager.GetRace(data.Race);
            _gender = GameManager.StaticInstance.ConfigsManager.GetGender(data.Gender);
            _visual = GameManager.StaticInstance.ConfigsManager.GetVisual(data.Visual);
            _voice = GameManager.StaticInstance.ConfigsManager.GetVoice(data.Voice);
            _faction = GameManager.StaticInstance.ConfigsManager.GetFaction(data.Faction);
            _statHolder = new(GameManager.StaticInstance.ConfigsManager.GetStatCreator(data.StatCreator));
            _stateHolder = new(GameManager.StaticInstance.ConfigsManager.GetStateCreator(data.StateCreator));
            _effectHolder = new(this);
            _inventory = new(this);
            foreach (KeyValuePair<string, int> stacks in data.ItemStacks)
            {
                _inventory.AddItem(GameManager.StaticInstance.ConfigsManager.GetItem(stacks.Key), stacks.Value);
            }
            _locomotion = new(this);
            _animator = LeanPool.Spawn(_visual.Model).GetComponent<PawnAnimator>();
            _animator.transform.SetPositionAndRotation(data.Transform.GetPosition(), data.Transform.GetRotation());
            _animator.Initialize(this);
            _created = true;
        }

        public void SaveData(ref PawnData data)
        {
            data.FirstName = _firstName.ID;
            data.LastName = _lastName.ID;
            data.Race = _race.ID;
            data.Gender = _gender.ID;
            data.Visual = _visual.ID;
            data.Voice = _voice.ID;
            data.Faction = _faction.ID;
            data.StatCreator = _data.StatCreator;
            data.StateCreator = _data.StateCreator;
            data.Transform.SetTransform(_animator.transform);
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
            // set weapon
            // set armor
            // set health
            // set stamina
        }
    }
}