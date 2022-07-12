using UnityEngine;


namespace PP.Game.Ability
{
    public class Ability_MedDroneDeploy : PP.Ability
    {
        public Ability_MedDroneDeploy(Pawn_Character pawn_char) : base(pawn_char)
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
            pawn_char.animator.SetTrigger("DeployMedDrone");

            return true;
        }
        public override void UseStock()
        {
            base.UseStock();

            GameObject gameObj_medDrone = GameObject.Find("MedDronePool").GetComponent<ObjPool>().PullItem();
            gameObj_medDrone.GetComponent<Pawn_MedDrone>().pawn_owner = pawn_char;
            Transform transform_doubleHand = pawn_char.transform.Find("DoubleHandle");
            gameObj_medDrone.transform.parent = transform_doubleHand;
            gameObj_medDrone.transform.position = transform_doubleHand.position;

            ((Pawn_PlayerCharacter)pawn_char).handledItem = gameObj_medDrone;
        }
    }
}