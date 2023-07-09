using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private int damage = 10;

    private List<Transform> targetRunners;

    protected override void SearchForTarget()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                    continue;

                runner.SetTarget();

                
                    targetRunners.Add(runner.transform);
                    Debug.Log("Add to list");
                    Debug.Log(targetRunners.Count);
                

                StartRunningTowardTarget();
                return;
            }
        }
    }

    protected override void RunTowardstarget()
    {
        if (targetRunners == null)
            return;

        for(int i = 0; i < targetRunners.Count; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetRunners[i].position, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, targetRunners[i].position) < 0.1f)
            {
                onRunnerDied?.Invoke();

                Destroy(targetRunners[i].gameObject);               
            }
        }

        if (targetRunners == null)
            return;

        Destroy(gameObject);
    }
}
