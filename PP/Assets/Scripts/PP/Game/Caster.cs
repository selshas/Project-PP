using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace PP.Game
{
    [System.Serializable]
    public class Caster : MonoBehaviour
    {
        [SerializeField] public Capacitor mp = new Capacitor();

        public float mpRegen = 0.02f;

        public UnityEvent<float> consumed = null;
        public UnityEvent<float> depleted = null;

        public float Consume(float value)
        {
            if (mp.current < value) value -= mp.current;
            mp.current -= value;

            CheckEventCall(value);

            return value;
        }

        public void PercentConsumeToMax(float damageValue_normalized) => Consume(Mathf.Max(1.0f, damageValue_normalized * mp.max));
        public void PercentConsumeToCurrent(float damageValue_normalized) => Consume(Mathf.Max(1.0f, damageValue_normalized * mp.current));
        private void CheckEventCall(float amountDamage)
        {
            if (mp.current <= 0) depleted?.Invoke(amountDamage);
            else consumed?.Invoke(amountDamage);
        }

        private void FixedUpdate()
        {
            if (mp.current < mp.max)
                mp.current = Mathf.Min(mp.max, mp.current + mpRegen);
        }
    }
}