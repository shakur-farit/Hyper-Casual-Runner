using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;

    private EnemyState enemyState = EnemyState.None;
    private Transform targetRunner;

    public static Action onRunnerDied;

    private void Start()
    {
        enemyState = EnemyState.Idle;
    }

    private void Update()
    {
        ManageEnemyState();
        
    }

    private void ManageEnemyState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                SearchForTarget();
                break;

            case EnemyState.Running:
                RunTowardstarget();
                break;
        }
    }

    private void SearchForTarget()
    {
        
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())                
                    continue;

                runner.SetTarget();
                targetRunner = runner.transform;

                StartRunningTowardTarget();
                return;
            }
        }
    }

    private void StartRunningTowardTarget()
    {
        enemyState = EnemyState.Running;
        GetComponent<Animator>().Play("Run");
    }

    private void RunTowardstarget()
    {
        if (targetRunner == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, Time.deltaTime * moveSpeed);

        if(Vector3.Distance(transform.position, targetRunner.position) < 0.1f)
        {
            onRunnerDied?.Invoke();

            Destroy(targetRunner.gameObject);
            Destroy(gameObject);
        }
    }
}
