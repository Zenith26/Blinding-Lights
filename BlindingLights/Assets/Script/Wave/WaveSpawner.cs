using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name; // name of the wave
        public GameObject enemy; // the enemy that's going to spawn
        public int count; // amount of enemies
        public float rate; // spawn rate
        public float timeTillNextWave = 20.0f; // time till the next wave
    }

    public Wave[] waves;

    // Wave[] waves index number
    private int waveIndex = -1;

    public Transform[] spawnPoints; // arrays of spawn position

    [SerializeField] float minDistance = 10;

    private int numberEnemySpawn = 0; // num of enemies that has been spawned in each wave, Reset once go to StartWave()

    private void Start()
    {
        if(spawnPoints.Length == 0) // Check if there is no spawn point
        {
            Debug.LogError("No Spawn points referenced");
        }

        StartWave();
    }

    void StartWave()
    {
        numberEnemySpawn = 0;
        CancelInvoke(); // Since there are no param, it will stop every function from invoke

        // if waveIndex is not passing through the max waves. Increase it
        if(waveIndex < waves.Length - 1)
        {
            waveIndex++;
        }
        Debug.Log("Wave: " + waves[waveIndex].name);

                    //function name   //time takes to spawn first one    // the next time takes to spawn
        InvokeRepeating("SpawnEnemy", waves[waveIndex].rate, waves[waveIndex].rate);

        // while it's still running InvokeRepeating. Call Invoke with the duration of timeTillNextWave
        Invoke("StartWave", waves[waveIndex].timeTillNextWave);
    }

    void SpawnEnemy()
    {
        // if the number of enemy that was spawned is less than the amount of enemy in a wave
        if(numberEnemySpawn >= waves[waveIndex].count)
        {
            return;
        }

        //ENEMY SPAWN
        Instantiate(waves[waveIndex].enemy, GetSpawnPointMinDistance(), Quaternion.identity); //enemy, spawnPos, spawnRot
        numberEnemySpawn++; // then it will increment until the if check above is false
    }

    //Vector3 GetSpawnPointMinDistance()
    //{
    //    List<Transform> _usableSpawn = new List<Transform>();

    //    // using player position, doesn't change, so created here
    //    Vector3 _playerLoc = GameManager.Instance.GetPlayer).transform.position;

    //    foreach(Transform point in spawnPoints) // for every Spawn Points
    //    {
    //        // getting distance between point and current spawn pos
    //        float newDistance = Vector3.Distance(point.position, _playerLoc);

    //        if(newDistance > minDistance) // if a spawner is further than the minDistance. Add it to list
    //        {
    //            _usableSpawn.Add(point);
    //        }

    //        //randomize between 0 and the usable spawn list
    //        int indexToUse = Random.Range(0, _usableSpawn.Count - 1);

    //        return _usableSpawn[indexToUse].transform.position; // return the chosen spawn position
    //    }
    //}
}
