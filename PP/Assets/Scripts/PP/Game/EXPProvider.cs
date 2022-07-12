using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP.Game
{
    public class EXPProvider : MonoBehaviour
    {
        public int totalExpPool = 1;

        public List<KeyValuePair<EXPGatherer, float>> list_kvp_gatherer_weight = new List<KeyValuePair<EXPGatherer, float>>();


        public void RegistGathererHistory(EXPGatherer gatherer, float weight)
        {
            list_kvp_gatherer_weight.Add(new KeyValuePair<EXPGatherer, float>(gatherer, weight));
        }

        public void Provide()
        {
            Dictionary<int, float> dict_weights = new Dictionary<int, float>();
            float weightSum = 0;
            for (int i = 0; i< list_kvp_gatherer_weight.Count;i++)
            {
                int uniqueID = list_kvp_gatherer_weight[i].Key.uniqueID;
                float weight = list_kvp_gatherer_weight[i].Value;

                if (dict_weights.ContainsKey(uniqueID))
                    dict_weights[uniqueID] += weight;
                else dict_weights.Add(uniqueID, weight);

                weightSum += weight;
            }

            Dictionary<int, float>.Enumerator enumerator = dict_weights.GetEnumerator();
            while (enumerator.MoveNext())
            {
                EXPGatherer.list_instances[enumerator.Current.Key].GainExp((int)(totalExpPool * enumerator.Current.Value / weightSum));
            }
        }
    }
}