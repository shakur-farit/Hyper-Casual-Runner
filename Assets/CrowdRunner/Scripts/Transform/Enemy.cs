using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float searchRadius;
    [SerializeField] protected float moveSpeed;

    protected EnemyState enemyState = EnemyState.None;
    protected Runner targetRunner;

    //public static Action onRunnerDied;

    private void Start()
    {
        enemyState = EnemyState.Idle;
    }

    protected void Update()
    {
        ManageEnemyState();
        
    }

    protected void ManageEnemyState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                SearchForTarget();
                break;

            case EnemyState.Running:
                RunTowardTarget();
                break;
        }
    }

    protected virtual void SearchForTarget()
    {
        
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())                
                    continue;

                runner.SetTarget();
                targetRunner = runner;

                StartRunningTowardTarget();
                return;
            }
        }
    }

    protected virtual void StartRunningTowardTarget()
    {
        enemyState = EnemyState.Running;

        if (GetComponent<Animator>() == null)
            return;

        GetComponent<Animator>().Play("Run");
    }

    protected virtual void RunTowardTarget()
    {
        if (targetRunner == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.transform.position, Time.deltaTime * moveSpeed);

        if(Vector3.Distance(transform.position, targetRunner.transform.position) < 0.1f)
        {
            //onRunnerDied?.Invoke();

            targetRunner.DestroyRunner();
            Destroy(gameObject);
        }
    }
}
