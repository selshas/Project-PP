using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    private Queue<GameObject> queue_stocks = new Queue<GameObject>();

    public int initNum = 20;
    public GameObject prefab;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i<initNum; i++) 
            queue_stocks.Enqueue(CreateNewItem());
    }

    GameObject CreateNewItem()
    {
        GameObject newObj = Instantiate(prefab, transform);
        (newObj.GetComponent<PoolItem>()?? newObj.AddComponent<PoolItem>()).parentPool = this;
        newObj.SetActive(false);
        return newObj;
    }

    public GameObject PullItem()
    {
        GameObject item;
        if (queue_stocks.Count == 0) 
            item = CreateNewItem();
        else
            item = queue_stocks.Dequeue();

        item.SetActive(true);
        item.transform.parent = null;
        return item;
    }

    public bool RetrieveItem(GameObject item)
    {
        if (item.GetComponent<PoolItem>().parentPool != this) return false;

        item.transform.parent = transform;
        item.SetActive(false);
        queue_stocks.Enqueue(item);
        return true;
    }

}