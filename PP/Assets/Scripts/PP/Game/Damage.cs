namespace PP.Game
{
    public struct Damage
    {
        public float value;
        public DamageType type;
        public float armorPierce;

        public Damage(float _value, DamageType _type = DamageType.kinetic, float _armorPierce = 0.0f)
        {
            value = _value;
            type = _type;
            armorPierce = _armorPierce;
        }
    }

}
