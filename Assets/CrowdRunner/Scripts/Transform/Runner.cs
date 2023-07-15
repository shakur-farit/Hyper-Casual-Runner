using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Runner : MonoBehaviour
{
    private bool isTarget;

    public static Action onRunnerDied;

    public void SetTarget()
    {
        isTarget = true;
    }

    public bool IsTarget()
    {
        return isTarget;
    }

    public void DestroyRunner()
    {
        onRunnerDied?.Invoke();
        Destroy(gameObject);
    }
}
