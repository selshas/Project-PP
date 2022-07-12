namespace PP
{
    enum LayerBit : int
    {
        Default             = 0b000000000000,
        TransparentFX       = 0b000000000001,
        IgnoreRaycast       = 0b000000000010,
        Projectile          = 0b000000000100,
        Water               = 0b000000001000,
        UI                  = 0b000000010000,
        CharacterEntity     = 0b000001000000,
        Terrain             = 0b000010000000,
        Pickup              = 0b000100000000,
        IgnoreCharacterPawn = 0b001000000000,
        Shield              = 0b010000000000,
        IgnoreProjectileOnly= 0b100000000000
    }
}