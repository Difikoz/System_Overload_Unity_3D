using System.Collections.Generic;

namespace WinterUniverse
{
    [System.Serializable]
    public class PawnSaveData
    {
        public string PawnName = "Loner";

        public string Faction = "Tramps";

        public float Health;
        public float Energy;

        public TransformValues Transform = new();
        public TransformValues RespawnTransform = new();

        public SerializableDictionary<string, int> InventoryStacks = new();

        public string WeaponInRightHand;
        public string WeaponInLeftHand;
        public List<string> Armors = new();
    }
}