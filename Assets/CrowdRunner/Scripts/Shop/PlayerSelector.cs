using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField] private Transform runnersParent;
    [SerializeField] private RunnerSelector runnerSelectorPrefab;

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SelectSkin(Random.Range(0, 3));
            Debug.Log("Change");
        }
    }

    public void SelectSkin(int skinIndex)
    {
        for (int i = 0; i < runnersParent.childCount; i++)
            runnersParent.GetChild(i).GetComponent<RunnerSelector>().SelectRunner(skinIndex);
        
        runnerSelectorPrefab.SelectRunner(skinIndex);
    }
}
