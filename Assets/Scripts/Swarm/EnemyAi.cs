using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] List<SwarmManager> enemySwarms;

    public Transform safeSpot;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void AllEnemiesRetreat()
    {
        for(int i = 0; i < enemySwarms.Count; i++)
        {
            enemySwarms[i].targetObject = safeSpot;
        }
    }
}
