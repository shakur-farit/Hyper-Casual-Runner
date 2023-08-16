using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUpgrade : MonoBehaviour
{

    public void Upgrade(int skinIndex)
    {
        Transform runnres = GameObject.Find("Runner Group").transform;
        for (int i = 0; i < runnres.childCount; i++)
        {
            runnres.GetChild(i).GetComponent<RunnerSelector>().SelectRunner(skinIndex);
        }
    }
}
