using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager instance;

    public PoolItem[] poolItems;

    public GameObject runnersParent;

    private Dictionary<int, Queue<GameObject>> poolQueue = new Dictionary<int, Queue<GameObject>>();
    private Dictionary<int, bool> growAbleBool = new Dictionary<int, bool>();
    private Dictionary<int, Transform> parents = new Dictionary<int, Transform>();

    private void Awake()
    {
        instance = this;

        PoolInit();
    }



    private void PoolInit()
    {
        GameObject poolGroup = new GameObject("Pool Group");

        for (int i = 0; i < poolItems.Length; i++)
        {
            GameObject uniquePool = new GameObject(poolItems[i].poolObject.name + " Group");
            uniquePool.transform.SetParent(poolGroup.transform);

            int objId = poolItems[i].poolObject.GetInstanceID();
            poolItems[i].poolObject.SetActive(false);

            poolQueue.Add(objId, new Queue<GameObject>());
            growAbleBool.Add(objId, poolItems[i].growAble);
            parents.Add(objId, uniquePool.transform);

            for (int j = 0; j < poolItems[i].poolAmount; j++)
            {
                GameObject temp = Instantiate(poolItems[i].poolObject, uniquePool.transform);
                poolQueue[objId].Enqueue(temp);
            }
        }
    }

    public GameObject UseObject(GameObject obj, Vector3 pos, Quaternion rot)
    {
        int objID = obj.GetInstanceID();

        GameObject temp = poolQueue[objID].Dequeue();

        if (temp.activeInHierarchy)
        {
            if (growAbleBool[objID])
            {
                poolQueue[objID].Enqueue(temp);
                temp = Instantiate(obj, parents[objID]);
                temp.transform.position = pos;
                temp.transform.rotation = rot;
                temp.SetActive(true);
            }
            else
            {
                temp = null;
            }
        }
        else
        {
            temp.transform.position = pos;
            temp.transform.rotation = rot;
            temp.SetActive(true);
        }

        poolQueue[objID].Enqueue(temp);

        if (temp.GetComponent<Runner>() != null)
            temp.transform.SetParent(runnersParent.transform);

        return temp;
    }

    public void ReturnObject(GameObject obj, float delay = 0f)
    {
        if (delay == 0f)
        {
            obj.SetActive(false);
        }
        else
        {
            StartCoroutine(DelayReturn(obj, delay));
        }

        if (obj.GetComponent<Runner>() != null)
        {
            obj.transform.SetParent(GameObject.Find("Runner Group").transform);
        }
    }

    private IEnumerator DelayReturn(GameObject obj, float delay)
    {
        while (delay > 0f)
        {
            delay -= Time.deltaTime;
            yield return null;
        }

        obj.SetActive(false);
    }
}


[System.Serializable]
public class PoolItem
{
    public GameObject poolObject; // obj of populate
    public int poolAmount; // start amount of obj
    public bool growAble; // can amoutn grow if start amout wasn't enough
}