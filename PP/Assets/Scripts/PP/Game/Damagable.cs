using UnityEngine;
using UnityEngine.Events;

namespace PP.Game
{
    [System.Serializable]
    public class Damagable : MonoBehaviour
    {
        public Pawn_Character owner;

        bool isDefeated = false;

        public Capacitor hp = new Capacitor
        {
            current = 10,
            max = 10
        };
        public Armor armor;
        public float armorMultiplyer;

        [SerializeField]
        public UnityEvent<float> damaged;
        [SerializeField]
        public UnityEvent<float> destroyed;

        public bool invulnerable = false;

        public void Start()
        {
            owner ??= GetComponent<Pawn_Character>();
        }

        public void OnEnable()
        {
            isDefeated = false;
        }

        public float Damage(Damage dmg)
        {
            if (isDefeated) return 0;
            if (invulnerable) return 0;

            //negative armorPierce means ignore armor 

            float reducedArmor = armor.value * armorMultiplyer - dmg.armorPierce;
            float effectiveDmg;
            /*
            effectiveDmg = (reducedArmor == 0) ? dmg.value : dmg.value * (1 - Mathf.Pow(2.0f, -(dmg.value / reducedArmor)));
            effectiveDmg = ((hp.current < effectiveDmg) ? (effectiveDmg - hp.current) : effectiveDmg);
            */
            effectiveDmg = dmg.value;
            hp.current -= effectiveDmg;

            CheckEventCall(effectiveDmg);

            return effectiveDmg;
        }

        private void CheckEventCall(float amountDamage)
        {
            if (isDefeated) return;

            if (hp.current <= 0) 
            {
                isDefeated = true;
                destroyed?.Invoke(amountDamage);
            }
            else damaged?.Invoke(amountDamage);
        }
    }
}