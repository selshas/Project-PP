using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


namespace PP.Game.Deployable
{
    public class TetherMine : MonoBehaviour
    {
        public Pawn_Character owner;

        PoolItem poolItem;
        Animator animator;

        public Damage damagePayload;
        public float activeRange;
        public float lifeTime = 10.0f;
        public int targetCount;

        int stage = 1;

        List<Pawn_Character> list_tetheredPawns = new List<Pawn_Character>();

        private void Awake()
        {
            poolItem ??= GetComponent<PoolItem>();
            animator ??= transform.Find("Appearance").GetComponent<Animator>();
        }

        public void Sense()
        {
            RaycastHit[] hits = Physics.BoxCastAll(
                transform.position,
                Vector3.one * activeRange,
                Vector3.up,
                transform.rotation,
                0,
                (int)(LayerBit.CharacterEntity)
            );

            if (hits.Length == 0) return;

            for (int i = 0; i < hits.Length; i++)
            {
                Damagable damagable = hits[i].collider.GetComponent<Damagable>();
                if (damagable == null) continue;

                if (damagable.owner.team != owner.team)
                {
                    stage = 2;
                    animator.SetTrigger("Trigger");
                    break;
                }
            }
        }

        public void Fire()
        {
            stage = 3;

            RaycastHit[] hits = Physics.BoxCastAll(
                transform.position,
                Vector3.one * activeRange,
                Vector3.up,
                transform.rotation,
                0,
                (int)(LayerBit.CharacterEntity)
            );

            if (hits.Length == 0) return;

            for (int i = 0; i < hits.Length; i++)
            {
                Damagable damagable = hits[i].collider.GetComponent<Damagable>();
                if (damagable == null) continue;

                if (damagable.owner.team != owner.team)
                {
                    list_tetheredPawns.Add(damagable.owner);
                    damagable.Damage(damagePayload);

                    if (targetCount > 0)
                    {
                        int j = 0;
                        for (; j < damagable.owner.list_statusEffects.Count; j++)
                            if (typeof(StatusEffect_Tether) == damagable.owner.list_statusEffects[j].GetType()) break;

                        if (j == damagable.owner.list_statusEffects.Count)
                        {
                            damagable.owner.AddStatusEffect(new StatusEffect_Tether(damagable.owner, lifeTime));
                            targetCount--;
                        }
                    }
                }
            }
        }

        public void TetherTargets()
        {
            lifeTime -= Time.fixedDeltaTime;
            if (lifeTime <= 0)
            {
                stage = 4;
                animator.SetTrigger("Trigger");
            }
        }

        private void FixedUpdate()
        {
            switch (stage)
            {
                case 0: // Deployed
                    {
                        break;
                    }
                case 1: // Armed
                    {
                        Sense();
                        break;
                    }
                case 2: // Triggered
                    {
                        break;
                    }
                case 3: // Fired
                    {
                        TetherTargets();
                        break;
                    }
                case 4: // Terminating
                    {
                        break;
                    }
            }
        }
    }
}


