using UnityEngine;
using UnityEngine.Animations;

namespace PP.Game
{
    [RequireComponent(typeof(Damagable), typeof(Caster))]
    public class Pawn_Zako : Pawn_Character
    {
        protected override void Awake()
        {
            base.Awake();
            team = 1;

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
        }

        public float attckCooldown_t;
        public float attackPeriod = 2.0f;

        override protected void Update()
        {
            base.Update();
            Move();
        }

        override protected void FixedUpdate()
        {
            base.FixedUpdate();
            if (attckCooldown_t < attackPeriod)
            {
                attckCooldown_t += Time.fixedDeltaTime; 
            }
        }
        public void OnEnable()
        {
            animator.SetTrigger("Initialize");
        }

        public void Fire()
        {
            if (attackPeriod < attckCooldown_t)
            {
                animator.SetTrigger("Fire");
                attckCooldown_t = 0;
            }
        }

        public void FireWeapon()
        {
            attckCooldown_t = 0;

            Vector3 barrelPos = transform.Find("GunBarrel").position;
            GameObject.Find("SFXPools/Gunfire").GetComponent<ObjPool>().PullItem().transform.position = barrelPos;

            GameObject item = GameObject.Find("ProjectilePool").GetComponent<ObjPool>().PullItem();
            Projectile bullet = item.GetComponent<Projectile>();
            bullet.pawn_owner = this;

            Projectile projectile = item.GetComponent<Projectile>();
            projectile.penetration = 1;
            projectile.damagePayload = new Damage(
                1
            );
            projectile.Fly(
                barrelPos,
                Vector3.right * faceDirection * 48.0f,
                1.5f
            );
        }

        public void OnDamaged()
        {
            animator.SetTrigger("Damaged");
        }

        public void Die()
        {
            list_statusEffects.Clear();

            PoolItem poolItem = GetComponent<PoolItem>();
            if (poolItem == null)
                Destroy(gameObject, 0.15f);
            else poolItem.Retrieve();
        }
    }
}