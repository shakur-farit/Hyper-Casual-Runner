using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Transform runnersParent;

    public void Run()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform runner = runnersParent.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Runner>()._Animator;

            runnerAnimator.Play("Run");
        }
    }

    public void Idle()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform runner = runnersParent.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Runner>()._Animator;

            runnerAnimator.Play("Idle");
        }
    }
}
