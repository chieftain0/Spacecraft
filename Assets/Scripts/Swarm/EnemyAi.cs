using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] List<SwarmManager> enemySwarms;

    public List<Transform> safeSpots;


    void Start()
    {
        AllEnemiesRetreat();
    }

    void Update()
    {
        
    }

    private void EnemyAttacks()
    {

    }

    private void AllEnemiesRetreat()
    {
        for(int i = 0; i < enemySwarms.Count; i++)
        {
            EnemyRetreats(enemySwarms[i]);
        }
    }

    private void EnemyRetreats(SwarmManager swarm)
    {
        swarm.targetObject = safeSpots[Random.Range(0, safeSpots.Count)];
    }

    private void EnemyDefends(SwarmManager swarm, Transform objectToDefend)
    {
        swarm.targetObject = objectToDefend;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 

        for (int i = 0; i < safeSpots.Count - 1; i++)
        {
            Gizmos.DrawLine(safeSpots[i].position, safeSpots[i + 1].position);
        }

        if (safeSpots.Count > 1)
        {
            Gizmos.DrawLine(safeSpots[safeSpots.Count - 1].position, safeSpots[0].position);
        }
    }
}
