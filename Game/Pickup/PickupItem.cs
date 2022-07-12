using UnityEngine;

namespace PP.Game
{
    public class PickupItem : MonoBehaviour
    {
        public bool isMagnetize => (magnetTarget != null);
        private Transform magnetTarget = null;

        [SerializeField]
        public UnityEngine.Events.UnityEvent<Pawn_Character> action;

        // Update is called once per frame
        void Update()
        {
            if (magnetTarget != null)
            {
                Vector3 deltaPos_pickupToGatherer = magnetTarget.transform.position - transform.position;
                Rigidbody rigidbody = GetComponent<Rigidbody>();
                rigidbody.AddForce(deltaPos_pickupToGatherer.normalized * 10, ForceMode.Force);
            }
        }

        public bool Magnetize(Transform target)
        {
            if (magnetTarget != null) return false;

            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.useGravity = false;
            magnetTarget = target;
            gameObject.layer = 6;

            return true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (action == null) return;

            Pawn_PlayerCharacter pc = other.GetComponent<Pawn_PlayerCharacter>();
            if (pc == null) return;

            action.Invoke(pc);
        }
    }
}