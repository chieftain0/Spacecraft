using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] List<WaveInfo> waveInfos;
    private List<SwarmManager> enemySwarms;
    [SerializeField] List<int> orderOfActivation;

    public List<Transform> safeSpots;
    private float timer = 0f;
    private float timeGoal;
    public int currentIndex = 0;

    [SerializeField] GameObject playerObject;

    void Start()
    {
        currentIndex = 0;
        //AllEnemiesRetreat();
        SpawnWave(waveInfos[currentIndex]);
        timeGoal = waveInfos[currentIndex].timerUntilNextActivation;
    }

    void Update()
    {
        if(timer > timeGoal & !waveInfos[currentIndex].isFinal)
        {
            currentIndex = currentIndex + 1;
            SpawnWave(waveInfos[currentIndex]);
            timer = 0f;
            timeGoal = waveInfos[currentIndex].timerUntilNextActivation;     
        }
        
        timer += Time.deltaTime;
        print("Timer " + timer + " time goal " + timeGoal);
    }

    void SpawnWave(WaveInfo waveInfo)
    {
      for(int i = 0; i < waveInfo.swarms.Count; i++)
      {
          waveInfo.swarms[i].gameObject.SetActive(true);
          EnemyAttacks(waveInfo.swarms[i], playerObject.transform);
      }
    }

    private void EnemyAttacks(SwarmManager swarm, Transform target)
    {
        swarm.targetObject = target;
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

[System.Serializable]
public class WaveInfo
{
    public List<SwarmManager> swarms;
    public float timerUntilNextActivation;
    public bool isFinal;
}
