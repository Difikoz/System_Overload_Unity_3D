namespace WinterUniverse
{
    [System.Serializable]
    public class PawnData
    {
        public string FirstName;
        public string LastName;
        public string Icon;
        public string Race;
        public string Gender;
        public string Visual;
        public string Voice;
        public string Faction;
        public string StatCreator;
        public string StateCreator;
        public TransformValues Transform;
        public SerializableDictionary<string, int> ItemStacks;
        public float Health;
        public float Stamina;
        public string Weapon;
        public string Armor;
    }
}