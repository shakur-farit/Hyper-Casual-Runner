using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

public class PlayerCollide : MonoBehaviour
{
    [SerializeField] private CrowdSystem crowdSystem;

    public static Action onDoorsHit;


    private void Update()
    {
        if(GameManager.instance.IsGameState())
            CollideDetected();
    }

    private void CollideDetected()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, crowdSystem.GetCrowdRadius());


        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Doors doors))
            {
                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.DisableDoors();

                onDoorsHit?.Invoke();

                crowdSystem.ApplyBonus(bonusAmount, bonusType);
            }
            else if (colliders[i].tag == "Finish")
            {
                // TO-DO; create LevelManager
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

                GameManager.instance.SetGameState(GameState.LevelComplete);
            }
            else if (colliders[i].tag == "Coin")
            {
                Destroy(colliders[i].gameObject);

                DataManager.instance.AddCoins(1);
            }
            else if(colliders[i].GetComponent<Boss>())
            {
                int damage = colliders[i].GetComponent<Boss>().damage;

                List<Runner> runners = colliders
                    .Select(collider => collider.GetComponent<Runner>())
                    .Where(runner => runner != null)
                    .ToList();

                for (int k = 0; k < damage && k < runners.Count; k++)
                {
                    runners[k].DestroyRunner();
                }

                Destroy(colliders[i].gameObject);
            }
        }
    }
}
