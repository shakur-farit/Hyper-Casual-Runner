using System;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [SerializeField] private PlayerAnimator animator;
    [SerializeField] private Transform runnersParent;
    [SerializeField] private GameObject runnerPrefab;

    [SerializeField] private float radius;
    [SerializeField] private float angle;

    void Update()
    {
        if (!GameManager.instance.IsGameState())
            return;

        PlaceRunners();

        if (runnersParent.childCount <= 0)
            GameManager.instance.SetGameState(GameState.GameOver);
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            runnersParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    // Calculate golden angle
    private Vector3 GetRunnerLocalPosition(int index)
    {

        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x,0,z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnersParent.childCount);
    }

    public void ApplyBonus(int bonusAmount, BonusType bonusType)
    {
        switch(bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                break;

            case BonusType.Product:
                int runnersToAdd = (runnersParent.childCount * bonusAmount) - runnersParent.childCount;
                AddRunners(runnersToAdd);
                break;

             case BonusType.Difference: 
                RemoveRunners(bonusAmount);
                break;

            case BonusType.Division:
                int runnersToRemove = runnersParent.childCount - (runnersParent.childCount/bonusAmount);
                RemoveRunners(runnersToRemove);
                break;

        }
    }

    private void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
            Instantiate(runnerPrefab, runnersParent);

        animator.Run();

        DecreaseRadius();
    }

    private void RemoveRunners(int amount)
    {
        if(amount>runnersParent.childCount)
            amount = runnersParent.childCount;

        int runnersAmount = runnersParent.childCount;

        for(int i = runnersAmount - 1; i >= runnersAmount - amount; i--)
        {
            Transform runnerToDeestroy = runnersParent.GetChild(i);
            runnerToDeestroy.SetParent(null);
            Destroy(runnerToDeestroy.gameObject);
        }

        IncreaseRadius();

    }

    private void IncreaseRadius()
    {
        if (runnersParent.childCount <= 25)
        {
            radius = 0.7f;
        }
        else if(runnersParent.childCount <= 50)
        {
            radius = 0.5f;
        }
        else if(runnersParent.childCount <= 100)
        {
            radius = 0.35f;
        }
        else
        {
            radius = 0.25f;
        }
    }

    private void DecreaseRadius()
    {
        if (runnersParent.childCount > 100)
        {
            radius = 0.25f;
        }
        else if (runnersParent.childCount > 50)
        {
            radius = 0.35f;
        }
        else if (runnersParent.childCount > 25)
        {
            radius = 0.5f;
        }
        else
        {
            radius = 0.7f;
        }
    }
}
