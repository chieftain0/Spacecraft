using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyAi : MonoBehaviour
{
    [SerializeField] List<WaveInfo> waveInfos;
    private List<SwarmManager> enemySwarms;
    [SerializeField] List<int> orderOfActivation;

    public List<Transform> safeSpots;
    private float timer = 0f;
    private float timePassed = 0f;
    private float timeGoal;
    public int currentIndex = 0;

    [Header("External stats")]
    private float totalEnemies = 0;
    public float totalScore = 0;
    public float enemiesKilled = 0;
    private float totalTime = 0;

    public string NextSceneToLoad;


    [SerializeField] GameObject playerObject;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI enemyCount;
    [SerializeField] TextMeshProUGUI scoreCount;
    [SerializeField] TextMeshProUGUI timeCount;

    public TextMeshProUGUI ControlModeUI;


    bool lastWaveSpawned = false;

    void Start()
    {
        currentIndex = 0;
        //AllEnemiesRetreat();
        //SpawnWave(waveInfos[currentIndex]);
        timeGoal = waveInfos[currentIndex].timerUntilNextActivation;

        CountStats();
        
        StartCoroutine(DisplayMissionText("KIll the enemies or survive", (waveInfos[currentIndex].timerUntilNextActivation) * 0.7f));
        
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        DisplayStats();


        if (timer > timeGoal & !waveInfos[currentIndex].isFinal)
        {
            
            SpawnWave(waveInfos[currentIndex]);

            currentIndex = currentIndex + 1;
            timer = 0f;
            timeGoal = waveInfos[currentIndex].timerUntilNextActivation;     
        }

        if (waveInfos[currentIndex].isFinal & !lastWaveSpawned & timer > timeGoal)
        {
            for (int i = 0; i < waveInfos[currentIndex].swarms.Count; i++)
            {
                waveInfos[currentIndex].swarms[i].gameObject.SetActive(true);
                EnemyAttacks(waveInfos[currentIndex].swarms[i], playerObject.transform);
            }

            //StopAllCoroutines();
            StartCoroutine(DisplayMissionText("New wave coming!", 1f));

            lastWaveSpawned = true;
        }

        if(waveInfos[currentIndex].isFinal & lastWaveSpawned)
        {
            LevelComplete();
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
        
        //StopAllCoroutines();
        StartCoroutine(DisplayMissionText("New wave coming!", 1f));
    }

    private void EnemyAttacks(SwarmManager swarm, Transform target)
    {
        swarm.targetObject = target;
    }

    void CountStats()
    {
        for (int i = 0; i < waveInfos.Count; i++)
        {
            for (int j = 0; j < waveInfos[i].swarms.Count; j++)
            {
                totalEnemies += waveInfos[i].swarms[j].spawnBoids;
            }

            totalTime += waveInfos[i].timerUntilNextActivation;
        }

        totalTime += Random.Range(50f, 70f);
    }

    private IEnumerator DisplayMissionText(string message, float time)
    {
        ControlModeUI.text = message;
        yield return new WaitForSeconds(time);
        ControlModeUI.text = string.Empty;

        Debug.LogError("Coroutine " + message);
    }

    void DisplayStats()
    {
        enemyCount.text = "Enemies killed " + enemiesKilled.ToString() + " out of " + totalEnemies.ToString();
        scoreCount.text = "Score " + totalScore.ToString();
        timeCount.text = "Time left " + (ConvertTime((int)(totalTime - timePassed)));
    }

    string ConvertTime(int seconds)
    {
        int minutes = seconds / 60;
        int remainingSeconds = seconds % 60;

        return minutes + " minutes and " + remainingSeconds + " seconds";
    }

    public void LevelComplete()
    {
        if((totalTime - timePassed) < 0f || enemiesKilled == totalEnemies)
        {
            Debug.LogError("Level is complete!");
            SceneManager.LoadScene("NextSceneToLoad");
        }
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
