namespace PP.Game
{
    [System.Serializable]
    public struct Armor
    {
        public ArmorType type;
        public float value;

        public Armor(float _value, ArmorType _type = ArmorType.Normal)
        {
            type = _type;
            value = _value;
        }
    }
}