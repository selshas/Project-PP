
namespace PP.Game.Ability
{
    public class Ability_PrecisionBombardment : PP.Ability
    {
        public Ability_PrecisionBombardment(PP.Game.Pawn_Character pawn_char) : base(pawn_char)
        {
            mpCost = new float[] {
                10, 10, 10, 10, 10,
                10, 10, 10, 10, 10
            };
            maxStock = new int[] {
                1, 1, 1, 2, 2,
                2, 3, 3, 3, 4
            };
            cooldown = new float[] {
                10, 10, 10, 10, 10,
                10, 10, 10, 10, 10
            };
            reloadTime = new float[] {
                10, 10, 10, 10, 10,
                10, 10, 10, 10, 10
            };
            modifier0 = new float[] {
                10, 10, 10, 10, 10,
                10, 10, 10, 10, 10
            };
            modifier1 = new float[] {
                10, 10, 10, 10, 10,
                10, 10, 10, 10, 10
            };

            currentStock = 1;
        }
        override public bool OnActive()
        {
            if (!base.OnActive()) return false;
            pawn_char.animator.SetTrigger("PlaceTether");

            return true;
        }
    }
}