using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private int damage = 10;

    private List<Transform> runnersToDestroy;

    protected override void SearchForTarget()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Runner runner))
            {
                Debug.Log(detectedColliders.Length);
                targetRunner = runner.transform;

                StartRunningTowardTarget();

                enemyState = EnemyState.Idle;
            }
        }
    }

    protected override void RunTowardTarget()
    {
        if (targetRunner == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, targetRunner.position) < 0.1f)
        {
            onRunnerDied?.Invoke();

            Destroy(targetRunner.gameObject);
            //Destroy(gameObject);
        }
    }
}
