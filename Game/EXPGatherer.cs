using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace PP.Game
{
    public class EXPGatherer : MonoBehaviour
    {
        public Pawn_Character pawn_owner;
        public int uniqueID { get; private set; } = -1;
        public static List<EXPGatherer> list_instances { get; private set; } = new List<EXPGatherer>();
        static Queue<int> queue_emptyIDs = new Queue<int>();

        public int level = 1; //{ get; private set; } = 1;
        public int exp = 0;// { get; private set; } = 0;

        public UnityEvent levelUp = null;

        private void Awake()
        {
            if (queue_emptyIDs.Count == 0)
            {
                uniqueID = list_instances.Count;
                list_instances.Add(this);
            }
            else
            {
                uniqueID = queue_emptyIDs.Dequeue();
                list_instances[uniqueID] = this;
            }

            pawn_owner ??= GetComponent<Pawn_Character>();
        }

        public void GainExp(int value)
        {
            exp += value;

            int currentCap = CurrentCap;
            if (currentCap <= exp)
            {
                level++;
                exp -= currentCap;
                pawn_owner.skillPoint++;
                levelUp?.Invoke();
            }
        }

        public int CurrentCap => Mathf.RoundToInt(Mathf.Log(level+1, 10) * 20);

        private void OnDestroy()
        {
            queue_emptyIDs.Enqueue(uniqueID);
        }
    }

}