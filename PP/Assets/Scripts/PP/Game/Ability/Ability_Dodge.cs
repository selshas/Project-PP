using UnityEngine;
using System;
using System.Collections;

namespace PP.Game.Ability
{
    public class Ability_Dodge : PP.Ability
    {
        public Ability_Dodge(PP.Game.Pawn_Character pawn_char) : base(pawn_char)
        {
            mpCost = new float[] {
                0
            };
            maxStock = new int[] {
                3
            };
            cooldown = new float[] {
                0.5f
            };
            reloadTime = new float[] {
                1.5f
            };
            modifier0 = new float[] {
                10
            };
            modifier1 = new float[] {
                10
            };

            currentStock = 3;
        }
        override public bool OnActive()
        {
            if (!base.OnActive()) return false;

            pawn_char.animator.SetTrigger("Dodge");
            pawn_char.GetComponent<Rigidbody>().velocity = (Vector3.right * pawn_char.faceDirection * 20.0f);
            pawn_char.AddStatusEffect(new StatusEffect_Dodge(pawn_char, 0.75f));

            UseStock();

            return true;
        }
    }
}