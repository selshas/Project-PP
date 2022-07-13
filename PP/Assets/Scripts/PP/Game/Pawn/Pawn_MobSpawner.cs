using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP.Game
{
    public class Pawn_MobSpawner : Pawn
    {
        [SerializeField]
        Capacitor stock = new Capacitor()
        {
            current = 5,
            max= 5
        };
        public float reloadTime = 10.0f;
        public float spawnCooldown = 5.0f;
        public float time_cooldownRemain = 0;
        public float time_cooldownReload = 0;
        public ObjPool pool;

        Vector3 sensorRange = new Vector3(10.0f, 8.0f, 8.0f);

        // Update is called once per frame
        override protected void FixedUpdate()
        {
            // Stock Depleted. Reload cycle
            if (stock.current <= 0)
            {
                time_cooldownReload -= Time.fixedDeltaTime;
                if (time_cooldownReload <= 0)
                    stock.current = stock.max;
            }
            // There is at least a remaining stock. procceed cooldown timer.
            else if (stock.current < stock.max)
            {
                time_cooldownRemain -= Time.fixedDeltaTime;
            }

            if (0 < stock.current && time_cooldownRemain <= 0 && Sense() != null)
            {
                stock.current--;
                if (stock.current <= 0) time_cooldownReload = reloadTime;
                else time_cooldownRemain = spawnCooldown;

                GameObject gameObj_newMob = pool.PullItem();
                gameObj_newMob.transform.position = transform.position;

                PP.Game.Damagable damagable = gameObj_newMob.GetComponent<PP.Game.Damagable>();
                damagable.hp.current = damagable.hp.max; 
            }
        }

        public GameObject Sense()
        {
            RaycastHit[] hits = Physics.BoxCastAll(
                transform.position,
                sensorRange,
                Vector3.up,
                transform.rotation,
                0,
                (int)(LayerBit.CharacterEntity)
            );

            if (hits.Length == 0) return null;

            foreach (RaycastHit hit in hits)
            {
                Pawn_Character pawn = hit.collider.GetComponent<Pawn_Character>();
                if (pawn == null) continue;
                if (pawn.team == team || pawn.team < 0) continue;

                return pawn.gameObject;
            }

            return null;
        }
    }

}