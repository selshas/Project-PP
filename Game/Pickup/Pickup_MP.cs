using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP.Game
{
    public class Pickup_MP : PickupItem
    {
        private void OnEnable()
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 10.0f + Vector3.right * Random.Range(-5, 5), ForceMode.Impulse);
        }
        public void ManaGen(Pawn_Character target)
        {
            Caster caster = target.GetComponent<Caster>();
            if (caster == null) return;

            caster.mp.current = Mathf.Min(caster.mp.max, caster.mp.current + 2);

            Destroy(gameObject);
        }
    }

}