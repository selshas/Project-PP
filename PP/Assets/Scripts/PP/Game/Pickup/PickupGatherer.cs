using UnityEngine;

namespace PP.Game
{
    public class PickupGatherer : MonoBehaviour
    {
        public Transform transform_magnet = null;
        new public Collider collider;
        public float magnetRange = 2.0f;

        private void Awake()
        {
            transform.hasChanged = false;
        }
        private void Update()
        {
            if (!transform.hasChanged) return;

            RaycastHit[] hits = Physics.BoxCastAll(
                transform.position,
                Vector3.one * magnetRange,
                Vector3.up,
                transform.rotation,
                0,
                0b100000000
            );

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    hits[i].collider.gameObject.GetComponent<PickupItem>()?.Magnetize((transform_magnet == null) ? transform : transform_magnet);
                }
            }

            transform.hasChanged = false;
        }
    }
}