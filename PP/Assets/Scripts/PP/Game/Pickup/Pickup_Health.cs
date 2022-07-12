using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP.Game
{
    public class Pickup_Health : PickupItem
    {
        private void OnEnable()
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up*10.0f + Vector3.right * Random.Range(-5,5), ForceMode.Impulse);
        }
        public void Heal(Pawn_Character target)
        {
            Damagable damagable = target.GetComponent<Damagable>();
            if (damagable == null) return;

            damagable.hp.current = Mathf.Min(damagable.hp.max, damagable.hp.current + 2);

            Destroy(gameObject);
        }
    }

}