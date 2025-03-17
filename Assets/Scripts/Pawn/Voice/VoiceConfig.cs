using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Voice", menuName = "Winter Universe/Pawn/New Voice")]
    public class VoiceConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private List<AudioClip> _greetings = new();
        [SerializeField] private List<AudioClip> _attacks = new();
        [SerializeField] private List<AudioClip> _getHits = new();
        [SerializeField] private List<AudioClip> _deaths = new();

        public string ID => _id;
        public List<AudioClip> Greetings => _greetings;
        public List<AudioClip> Attacks => _attacks;
        public List<AudioClip> GetHits => _getHits;
        public List<AudioClip> Deaths => _deaths;
    }
}