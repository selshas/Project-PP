namespace PP
{
    public abstract class Ability : IAbility
    {
        AbilityInfo info = null;

        public float[] mpCost = {
            10, 10, 10, 10, 10,
            10, 10, 10, 10, 10
        };
        public int[] maxStock = {
            10, 10, 10, 10, 10,
            10, 10, 10, 10, 10
        };
        public float[] cooldown = {
            10, 10, 10, 10, 10,
            10, 10, 10, 10, 10
        };
        public float[] reloadTime = {
            10, 10, 10, 10, 10,
            10, 10, 10, 10, 10
        };
        public float[] modifier0 = {
            10, 10, 10, 10, 10,
            10, 10, 10, 10, 10
        };
        public float[] modifier1 = {
            10, 10, 10, 10, 10,
            10, 10, 10, 10, 10
        };
        static protected int maxLevel = 10;


        protected PP.Game.Pawn_Character pawn_char;

        public int level = 0;
        public int level_max = 9;
        public int currentStock = 1;
        public float time_reload = 0;
        public float time_cooldown = 0;


        public Ability(Game.Pawn_Character pawn_owner)
        {
            pawn_char = pawn_owner;
            currentStock = maxStock[level];
        }
        public virtual bool IsAvailable()
        {
            if (0 < mpCost[level])
            {
                PP.Game.Caster caster = pawn_char.GetComponent<Game.Caster>();

                if (caster == null) return false;
                if (caster.mp.current < mpCost[level]) return false;
            }

            if (0 < time_cooldown) return false;
            if (currentStock <= 0) return false;

            return true;
        }
        public virtual bool OnActive() => IsAvailable();
        public virtual void UseStock()
        {
            currentStock--;

            if (currentStock <= maxStock[level] && time_reload <= 0)
                time_reload = reloadTime[level];
            else
                time_cooldown = cooldown[level];

            Game.Caster caster = pawn_char.GetComponent<Game.Caster>();
            if (caster != null) caster.mp.current -= mpCost[level];
        }

        public void ForceCooldown() => time_cooldown = cooldown[level];
    }
}