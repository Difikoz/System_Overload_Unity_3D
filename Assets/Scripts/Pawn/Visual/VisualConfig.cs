using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Visual", menuName = "Winter Universe/Pawn/New Visual")]
    public class VisualConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private GameObject _model;
        [SerializeField] private AnimatorOverrideController _controller;

        public string ID => _id;
        public GameObject Model => _model;
        public AnimatorOverrideController Controller => _controller;
    }
}