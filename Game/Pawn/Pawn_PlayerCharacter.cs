using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP.Game
{

    [RequireComponent(typeof(Damagable), typeof(Caster), typeof(EXPGatherer))]
    public class Pawn_PlayerCharacter : Pawn_Character
    {
        public GameObject handledItem;

        EXPGatherer expGatherer;

        Ability.Ability_TetherMine ability_tether;
        Ability.Ability_GunShield ability_gunShield;
        Ability.Ability_MedDroneDeploy ability_medDrone;
        Ability.Ability_PrecisionBombardment ability_precisionBombardment;
        Ability.Ability_Dodge ability_dodge;

        override protected void Awake()
        {
            base.Awake();

            expGatherer = GetComponent<EXPGatherer>();


            abilities = new PP.Ability[5];

            ability_tether = new Ability.Ability_TetherMine(this);
            ability_gunShield = new Ability.Ability_GunShield(this);
            ability_medDrone = new Ability.Ability_MedDroneDeploy(this);
            ability_precisionBombardment = new Ability.Ability_PrecisionBombardment(this);
            ability_dodge = new Ability.Ability_Dodge(this);

            abilities[0] = ability_gunShield;
            abilities[1] = ability_tether;
            abilities[2] = ability_medDrone;
            abilities[3] = ability_precisionBombardment;
            abilities[4] = ability_dodge;
        }

        override protected void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public void Fire()
        {
            Vector3 barrelPos = transform.Find("GunBarrel").position;
            GameObject.Find("SFXPools/Gunfire").GetComponent<ObjPool>().PullItem().transform.position = barrelPos;

            GameObject item = GameObject.Find("ProjectilePool").GetComponent<ObjPool>().PullItem();
            Projectile bullet = item.GetComponent<Projectile>();
            bullet.pawn_owner = this;

            Projectile projectile = item.GetComponent<Projectile>();

            projectile.penetration = 1;
            projectile.damagePayload = new Damage(
                1 + Mathf.Log10(expGatherer.level)
            ); ;
            projectile.Fly(
                barrelPos,
                Vector3.right * faceDirection * 48.0f,
                1.5f
            );
        }

        public void SpawnTetherMine() => ability_tether.UseStock();
        public void SpawnMedDrone() => ability_medDrone.UseStock();

        public void ReleaseMedDrone()
        {
            handledItem.transform.parent = null;
            handledItem = null;
        }

        public void RetractGunShield()
        {
            animator.SetBool("DeployGunShield", false);
            ability_gunShield.Retract();
            ability_gunShield.UseStock();
        }

        public void BeDefeated()
        {
            animator.SetTrigger("Defeated");
        }

    }
}