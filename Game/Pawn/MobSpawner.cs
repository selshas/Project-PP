using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public int stock = 5;
    public int stock_max= 5;
    public float reloadTime = 10.0f;
    public float spawnCoolDown = 5.0f;
    public float time = 5.0f;
    public ObjPool pool;

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.fixedDeltaTime;
        if (time > 0) return;

        if (stock <= 0)
        {
            stock = stock_max;
            time = spawnCoolDown + time;
        }
        else
        {
            time = spawnCoolDown + time;
            stock--;

            GameObject gameObj_newMob = pool.PullItem();
            gameObj_newMob.transform.position = transform.position;

            PP.Game.Damagable damagable = gameObj_newMob.GetComponent<PP.Game.Damagable>();
            damagable.hp.current = damagable.hp.max;
        }
    }
}
