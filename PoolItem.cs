using UnityEngine;

public class PoolItem : MonoBehaviour
{
    public ObjPool parentPool;
    public void Retrieve()
    {
        //Debug.Log("Retrieving");
        parentPool.RetrieveItem(gameObject);
    }
}