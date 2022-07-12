using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.Animations;

namespace PP.Game
{
    [RequireComponent(typeof(Rigidbody), typeof(PoolItem), typeof(BoxCollider))]
    public class Projectile : MonoBehaviour
    {
        public Damage damagePayload;

        Vector3 prevPos = Vector3.zero;

        public Pawn_Character pawn_owner = null;
        Rigidbody rigidbody = null;
        PoolItem poolItem = null;
        BoxCollider boxCollider = null;

        Transform transform_appearance;

        public int penetration = 1;

        float lifeTime;
        float endTime;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            poolItem = GetComponent<PoolItem>();
            boxCollider = GetComponent<BoxCollider>();
            transform_appearance = transform.Find("Appearance");
        }

        // Start is called before the first frame update
        void Start()
        {
            ConstraintSource source = new ConstraintSource()
            {
                sourceTransform = Camera.main.transform,
                weight = 1.0f
            };

            RotationConstraint rotConstraint = transform_appearance.GetComponent<RotationConstraint>();
            if (0 == rotConstraint.sourceCount)
            {
                rotConstraint.locked = false;
                rotConstraint.AddSource(source);
                rotConstraint.constraintActive = true;
                rotConstraint.locked = true;
            }
        }

        public void Fly(Vector3 from, Vector3 direction, float lifeTime)
        {
            transform.position = from;
            prevPos = from;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(direction, ForceMode.VelocityChange);
            endTime = lifeTime;
            this.lifeTime = 0;

            if (direction.x < 0)
            {
                Vector3 scale = transform_appearance.localScale;
                scale.x = -1;
                transform_appearance.localScale = scale;
            }
            else
            {
                Vector3 scale = transform_appearance.localScale;
                scale.x = 1;
                transform_appearance.localScale = scale;
            }
            transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
        }

        public void FixedUpdate()
        {
            lifeTime += Time.fixedDeltaTime;

            if (transform.localScale.x < 1.0f)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Min(1.0f, lifeTime / 0.05f);
                transform.localScale = scale;
            }

            if (endTime <= lifeTime)
                poolItem.Retrieve();

            prevPos = transform.position;
        }
        private void OnTriggerEnter(Collider other)
        {
            Vector3 deltaPosition = (transform.position - prevPos);
            RaycastHit[] hits = Physics.BoxCastAll(
                prevPos + boxCollider.center - deltaPosition,
                boxCollider.size / 2,
                deltaPosition.normalized,
                transform.rotation,
                deltaPosition.magnitude,
                0b10011000000
            );

            foreach (RaycastHit hit in hits.OrderBy(x => x.distance))
            {
                int layerBit = 1 << hit.collider.gameObject.layer;
                Damagable damagable;
                if ((layerBit & (int)LayerBit.CharacterEntity) != 0 || (layerBit & (int)LayerBit.Shield) != 0)
                {
                    damagable = hit.collider.GetComponent<Damagable>();
                    if (damagable == null) continue;
                    if (damagable.owner == pawn_owner) continue;

                    Pawn_Character pawn_shot = damagable.owner.GetComponent<Pawn_Character>();
                    //if (pawn_shot == null) Debug.Log(damagable.owner.gameObject.name);
                    if (damagable.owner == null) continue;
                    if (damagable.owner.team == pawn_owner.team) continue;

                    float edmg = damagable.Damage(damagePayload);
                    
                    if (pawn_owner == null) continue;
                    EXPGatherer expGatherer = pawn_owner.GetComponent<EXPGatherer>();
                    EXPProvider expProvider = pawn_shot.GetComponent<EXPProvider>();

                    if (expGatherer != null && expProvider != null) 
                        expProvider.RegistGathererHistory(expGatherer, edmg);

                    penetration--;
                    if (penetration <= 0)
                    {
                        poolItem.Retrieve();
                        break;
                    }
                }
                else if ((layerBit & (int)(LayerBit.Terrain)) != 0)
                {
                    poolItem.Retrieve();
                }
            }
        }
    }
}