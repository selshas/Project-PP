using UnityEngine;
using UnityEngine.Animations;

namespace PP.Game
{
    public class Pawn_MedDrone : Pawn
    {
        static Pawn_MedDrone lastOne = null;
        public Pawn_Character pawn_owner;

        public float speed = 0.2f;
        public float healAmount;
        public float duration;

        public float elevationThrust = 1.0f;
        public float elevationThreshord = 1.0f;

        public float lifeTime_remained = 10;

        public bool isOperating = false;

        bool isHealRayActivated = false;

        public GameObject gameObj_beamTarget;
        public GameObject gameObj_fx;

        private void Start()
        {
            lastOne?.Despawn();

            ConstraintSource source = new ConstraintSource()
            {
                sourceTransform = Camera.main.transform,
                weight = 1.0f
            };

            RotationConstraint rotConstraint = transform.Find("Appearance").GetComponent<RotationConstraint>();
            if (0 == rotConstraint.sourceCount)
            {
                rotConstraint.locked = false;
                rotConstraint.AddSource(source);
                rotConstraint.constraintActive = true;
                rotConstraint.locked = true;
            }
            lastOne = this;
        }

        override protected void FixedUpdate()
        {
            base.FixedUpdate();
            if (!isOperating) return; 

            // Adjust Thruster according to input command.
            if (0 == inputVector_moving.y)
                elevationThrust = 1.0f;
            else
            {
                if (0 < inputVector_moving.y)
                    elevationThrust = 2.0f;
                else
                    elevationThrust = 0.0f;

                // Vertical Thruster
                GetComponent<Rigidbody>().AddForce(elevationThrust * -Physics.gravity, ForceMode.Acceleration);
            }

            Move();

            lifeTime_remained -= Time.fixedDeltaTime;
            if (lifeTime_remained <= 0) Despawn();
            if (isHealRayActivated) HealRay();
        }

        public void HealRay()
        {
            Damagable damagable = pawn_owner.GetComponent<Damagable>();
            if (damagable.hp.current <= 0) return; 

            Vector3 deltaPos = pawn_owner.transform.position - transform.Find("Appearance/Beam").position;
            deltaPos.x *= faceDirection;
            deltaPos.y += 1.5f;

            gameObj_fx.transform.position = pawn_owner.transform.position;

            transform.Find("Appearance/Beam/Line").GetComponent<LineRenderer>().SetPositions(new Vector3[2] {
                Vector3.zero,
                deltaPos
            });

            damagable.hp.current = Mathf.Min(damagable.hp.max, damagable.hp.current + 0.02f);
        }

        public void OnEnable()
        {
            transform.Find("Appearance/Beam").gameObject.SetActive(false);
        }

        public void ActivateHealRay()
        {
            transform.Find("Appearance/Beam").gameObject.SetActive(true);
            gameObj_fx.SetActive(true);
            gameObj_fx.GetComponent<ParticleSystemGroupControl>().Play();
            isHealRayActivated = true;

            HealRay();
        }
        public void DeactivateHealRay()
        {
            transform.Find("Appearance/Beam").gameObject.SetActive(false);
            gameObj_fx.GetComponent<ParticleSystemGroupControl>().Stop();
            isHealRayActivated = false;
        }

        public void Despawn()
        {
            Destroy(gameObject);
            lastOne = null;
        }
    }
}


