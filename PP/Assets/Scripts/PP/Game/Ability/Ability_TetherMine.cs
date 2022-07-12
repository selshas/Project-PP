using UnityEngine;

namespace PP.Game.Ability
{
    public class Ability_TetherMine : PP.Ability
    {
        float[] modifier2;
        int[] modifier3;
        public Ability_TetherMine(PP.Game.Pawn_Character pawn_char) : base(pawn_char)
        {
            mpCost = new float[] {
                10, 10, 10, 10, 10,
                10, 10, 10, 10, 10
            };
            maxStock = new int[] {
                2, 2, 2, 3, 3,
                3, 3, 3, 4, 4
            };
            cooldown = new float[] {
                5.0f, 4.8f, 4.6f, 4.4f, 4.2f,
                4.0f, 3.8f, 3.6f, 3.2f, 3.0f
            };
            reloadTime = new float[] {
                10.0f, 9.6f, 9.2f, 9.0f, 8.8f,
                8.6f, 8.4f, 8.2f, 8.0f, 8.0f
            };
            // damage
            modifier0 = new float[] {
                1.0f, 1.2f, 1.4f, 1.5f, 1.5f,
                1.5f, 1.7f, 1.8f, 1.9f, 2.0f
            };
            // duration
            modifier1 = new float[] {
                3.0f, 3.2f, 3.4f, 3.6f, 3.8f,
                4.0f, 4.2f, 4.4f, 4.6f, 4.8f
            };
            //range
            modifier2 = new float[] {
                3.0f, 3.2f, 3.4f, 3.6f, 3.8f,
                4.0f, 4.2f, 4.4f, 4.6f, 4.8f
            };
            // count
            modifier3 = new int[] {
                3, 3, 3, 3, 4,
                4, 4, 5, 5, 6
            };

            currentStock = 2;
        }
        override public bool OnActive()
        {
            if (!base.OnActive()) return false;
            pawn_char.animator.SetTrigger("PlaceTether");

            return true;
        }

        public override void UseStock()
        {
            base.UseStock();

            GameObject item = GameObject.Find("TetherMinePool").GetComponent<ObjPool>().PullItem();
            PP.Game.Deployable.TetherMine mine = item.GetComponent<PP.Game.Deployable.TetherMine>();
            item.transform.position = pawn_char.transform.position;
            mine.owner = pawn_char;
            mine.damagePayload = new PP.Game.Damage(
                modifier0[level]
            );
            mine.lifeTime = modifier1[level];
            mine.activeRange = modifier2[level];
            mine.targetCount = modifier3[level];
        }
    }
}