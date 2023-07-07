using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float searchRadius;

    private Transform targetRunner;

    [SerializeField] private float rotSpeed;

    // Update is called once per frame
    void Update()
    {
        DestroyDetectedRunner();
        AutoRotate();
    }

    private void DestroyDetectedRunner()
    {

        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            
            if (detectedColliders[i].TryGetComponent(out Runner runner))
            {
                Debug.Log(detectedColliders.Length);
                targetRunner = runner.transform;

                Destroy(targetRunner.gameObject);
            }
        }
    }

    private void AutoRotate()
    {
        transform.Rotate(1 * rotSpeed, 0, 0);
    }
}
