using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Damage Type", menuName = "Winter Universe/Misc/New Damage Type")]
    public class DamageTypeConfig : BasicInfoConfig
    {
        //[SerializeField] private StatConfig _resistanceStat;
        [SerializeField] private List<GameObject> _hitVFX = new();
        [SerializeField] private List<AudioClip> _hitClips = new();

        //public StatConfig ResistanceStat => _resistanceStat;
        public List<GameObject> HitVFX => _hitVFX;
        public List<AudioClip> HitClips => _hitClips;
    }
}