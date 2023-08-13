using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSelector : MonoBehaviour
{
    [SerializeField] private Runner runner;

    public void SelectRunner(int skinIndex)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == skinIndex)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                runner._Animator = transform.GetChild(i).GetComponent<Animator>();
            }
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
