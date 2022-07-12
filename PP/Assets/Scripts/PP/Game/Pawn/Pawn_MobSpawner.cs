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
        public float time = 0;
        public ObjPool pool;

        Vector3 sensorRange = new Vector3(10.0f, 8.0f, 8.0f);

        // Update is called once per frame
        override protected void FixedUpdate()
        {
            // Stock Depleted. Reload cycle
            if (stock.current <= 0)
            {
                time += Time.fixedDeltaTime;
                if (reloadTime <= time)
                {
                    time = spawnCooldown;
                    stock.current = stock.max;
                }
            }

            if (stock.current <= 0) return;

            // There is at least a remaining stock. procceed cooldown timer.
            if (stock.current < stock.max)
                time += Time.fixedDeltaTime;

            if (spawnCooldown <= time && Sense() != null)
            {
                time = spawnCooldown - time;
                stock.current--;

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