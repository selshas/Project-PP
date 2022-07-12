using UnityEngine;
using System.Collections.Generic;

namespace PP.Game
{
    [RequireComponent(typeof(Damagable), typeof(Caster))]
    public class Pawn_Character : Pawn
    {
        public int skillPoint = 0;

        Damagable damagable = null;
        Caster caster = null;

        public List<StatusEffect> list_statusEffects { get; private set; }  = new List<StatusEffect>();

        public Pawn_Character()
        {
            mobility = true;
        }

        protected override void Awake()
        {
            base.Awake();

            damagable ??= GetComponent<Damagable>();
            caster ??= GetComponent<Caster>();
        }

        public PP.Ability[] abilities = new PP.Ability[0];

        public void UseAbility(int no) => abilities[no].OnActive();
        
        public void AddStatusEffect(StatusEffect statusEffect)
        { 
            list_statusEffects.Add(statusEffect);
        }

        public void InjectSkillPoint(int abilityNo)
        {
            if (skillPoint <= 0) return;
            if (abilities[abilityNo].level >= abilities[abilityNo].level_max) return;

            skillPoint--;
            abilities[abilityNo].level++;
        }

        override protected void FixedUpdate()
        {
            base.FixedUpdate();
            if (list_statusEffects.Count > 0)
            {
                for (int i = 0; i<list_statusEffects.Count;i++) 
                {
                    StatusEffect statusEffect = list_statusEffects[i];

                    if (!statusEffect.isSustained) statusEffect.OnStart();

                    statusEffect.OnSustain();
                    statusEffect.Tick();

                    if (statusEffect.lifeTime <= 0)
                    {
                        list_statusEffects.RemoveAt(i);
                        i--;
                    }
                }
            }

            for (int i = 0; i < abilities.Length; i++)
            {
                if (abilities[i].currentStock < abilities[i].maxStock[abilities[i].level])
                {
                    abilities[i].time_reload -= Time.fixedDeltaTime;
                    if (abilities[i].time_reload <= 0)
                    {
                        abilities[i].currentStock++;
                        abilities[i].time_reload = abilities[i].reloadTime[abilities[i].level] + abilities[i].time_reload;
                    }
                }

                if (abilities[i].time_cooldown > 0)
                    abilities[i].time_cooldown -= Time.fixedDeltaTime;
            }
        }
    }
}