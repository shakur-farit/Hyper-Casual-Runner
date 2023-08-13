using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Runner : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool isTarget;

    public static Action onRunnerDied;

    public bool IsTarget => isTarget;

    public Animator _Animator { get { return animator; } set { animator = value; } }

    public void SetTarget()
    {
        isTarget = true;
    }

    public void DestroyRunner()
    {
        onRunnerDied?.Invoke();

        PoolingManager.instance.ReturnObject(gameObject);
    }
}
