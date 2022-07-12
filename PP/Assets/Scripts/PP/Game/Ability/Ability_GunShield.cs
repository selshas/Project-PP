using UnityEngine;

namespace PP.Game.Ability
{
    public class Ability_GunShield : PP.Ability
    {
        private bool _isDeployed = false;
        StatusEffect_GunShieldDeployed statusEffect;

        public Ability_GunShield(PP.Game.Pawn_Character pawn_char) : base(pawn_char)
        {
            mpCost = new float[] {
                5, 5, 5, 5, 5,
                5, 5, 5, 5, 5
            };
            maxStock = new int[] {
                1, 1, 1, 1, 1,
                1, 1, 1, 1, 1
            };
            cooldown = new float[] {
                3, 3, 3, 2.6f, 2.6f,
                2.4f, 2.4f, 2.2f, 2.2f, 2
            };
            reloadTime = new float[] {
                3, 3, 3, 2.6f, 2.6f,
                2.4f, 2.4f, 2.2f, 2.2f, 2
            };
            modifier0 = new float[] {
                15, 17, 18, 19, 20,
                21, 22, 23, 24, 25
            };

            currentStock = 1;

            statusEffect = new StatusEffect_GunShieldDeployed(pawn_char, 10);
        }
        override public bool OnActive()
        {
            if (!base.OnActive()) return false;

            pawn_char.animator.SetTrigger("ToggleGunShield");

            if (_isDeployed) Retract();
            else Deploy();

            UseStock();

            return true;
        }

        public void Deploy()
        {
            pawn_char.animator.SetBool("DeployGunShield", true);
            _isDeployed = true;

            GameObject gameObj_gunShield = pawn_char.transform.Find("GunBarrel/GunShield").gameObject;
            gameObj_gunShield.gameObject.SetActive(true);
            Damagable damagable = gameObj_gunShield.GetComponent<Damagable>();
            damagable.hp.current = modifier0[level];
            damagable.hp.max = modifier0[level];

            pawn_char.AddStatusEffect(statusEffect);
        }

        public void Retract()
        {
            pawn_char.animator.SetBool("DeployGunShield", false);
            _isDeployed = false;

            Caster caster = pawn_char.GetComponent<Game.Caster>();
            if (caster != null) caster.mp.current += mpCost[level];
            GameObject gameObj_gunShield = pawn_char.transform.Find("GunBarrel/GunShield").gameObject;
            gameObj_gunShield.SetActive(false);

            statusEffect.OnEnd();
            pawn_char.list_statusEffects.Remove(statusEffect);
        }
    }
}