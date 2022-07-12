using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    public List<GameObject> list_items = new List<GameObject>();
    public List<float> list_weights = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        // Normalizing
        float total = 0;
        foreach (float weight in list_weights)
        {
            total += weight;
        }
        if (total > 1.0f)
        {
            for (int i = 0; i<list_weights.Count;i++)
            {
                list_weights[i] /= total;
            }
        }

    }

    public void Gatcha()
    {
        for (int i = 0; i < list_weights.Count; i++)
        {
            float rand = Random.Range(0, 1.0f);
            if (list_weights[i] > rand)
            {
                Instantiate(list_items[i]).transform.position = transform.position;
            }
        }
    }
}
