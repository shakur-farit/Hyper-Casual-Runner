using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float searchRadius;

    [SerializeField] private bool isMoving;
    [SerializeField] private bool isRotate;

    [SerializeField] private float rotSpeed;
    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject[] wayPoints;
    private int currentPoint = 0;
    private float WPradius = 1;

    private void FixedUpdate()
    {
        if (isRotate)
            AutoRotate();

        if (isMoving)
            AutoMove();
    }

    void Update()
    {
        DestroyDetectedRunner();
    }

    private void DestroyDetectedRunner()
    {

        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            
            if (detectedColliders[i].TryGetComponent(out Runner runner))
            {
                Debug.Log(detectedColliders.Length);
                runner.DestroyRunner();
            }
        }
    }

    private void AutoRotate()
    {
        transform.Rotate(1 * rotSpeed, 0, 0);
    }

    private void AutoMove()
    {
        if (Vector3.Distance(wayPoints[currentPoint].transform.position, transform.position) < WPradius)
        {
            currentPoint++;
            if (currentPoint >= wayPoints.Length)
                currentPoint = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentPoint].transform.position, Time.deltaTime * moveSpeed);
    }
}
