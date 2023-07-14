using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public int damage = 10;

    protected override void RunTowardTarget()
    {
        if (targetRunner == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, Time.deltaTime * moveSpeed);
    }
}
