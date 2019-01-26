using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnController : MonoBehaviour
{
    public Transform straightBulletPrefab;
    public Transform spinBulletPrefab;
    public Transform zigzagBulletPrefab;
    public List<Transform> spawnerList;
    public List<Wave> wavePatterns;

    public Wave randomWave;

    public int spawnsMin = 1;
    public int spawnsMax = 5;
    public int RandomWaveBulletsPerSpawnMin = 1;
    public int RandomWaveBulletsPerSpawnMax = 3;
    public float RandomWaveDelayBetweenBulletsMin = (float)0.3;
    public float RandomWaveDelayBetweenBulletsMax = (float)1;
    public float RandomWaveDelayBetweenSpawnsMin = (float)0.3;
    public float RandomWaveDelayBetweenSpawnsMax = (float)1;

    public static readonly string STRAIGHTWAVE = "STRAIGHTWAVE";
    public static readonly string SPINWAVE = "SPINWAVE";
    public static readonly string ZIGWAVE = "ZIGWAVE";

    void Update()
    {
        //Just for Debugging!
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnWaveFromString("StraightTopAndBottomEach5");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnRandomWaveFromParametersUsingPrefab(straightBulletPrefab);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SpawnRandomWaveFromParametersUsingPrefab(spinBulletPrefab);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            SpawnRandomWaveFromParametersUsingPrefab(zigzagBulletPrefab);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SpawnRandomWaveFromPool();
        }
    }

    IEnumerator SpawnWave(Wave w)
    {
        Debug.Log("SpawnWave: Prefab" + w.bulletPrefab + " nSpawns " + w.spawnindexArray.Length + " numberBulletsPerSpawn " + w.numberBulletsPerSpawn
        + " delayBetweenBullets " + w.delayBetweenBullets + " delayBetweenSpawns " + w.delayBetweenSpawns);

        if (w.bulletPrefab == null)
            Debug.LogError("No Prefab found at SpawnWave!");

        for (int nBullet = 0; nBullet < w.numberBulletsPerSpawn; nBullet++)
        {
            for (int i = 0; i < w.spawnindexArray.Length; i++)
            {
                int spawnindexValue = w.spawnindexArray[i];
                if (spawnindexValue <= spawnerList.Count - 1)
                    Instantiate(w.bulletPrefab, spawnerList[spawnindexValue].position, spawnerList[spawnindexValue].rotation);

                yield return new WaitForSeconds(w.delayBetweenSpawns);
            }
            yield return new WaitForSeconds(w.delayBetweenBullets);
        }
    }

    public void SpawnRandomWaveFromPool()
    {
        if (wavePatterns.Count == 0) return;

        int index;
        index = Random.Range(0, wavePatterns.Count);

        Debug.Log("Random Wave index" + index + "/" + (wavePatterns.Count - 1));
        if (wavePatterns[index] == null)
        {
            Debug.LogError("Could not Spawn Wave");
        }
        else
        {
            StartCoroutine(SpawnWave(wavePatterns[index]));
        }
    }

    public void SpawnRandomWaveFromParametersUsingPrefab(Transform prefab)
    {
        int numberBulletsPerSpawn = Random.Range(RandomWaveBulletsPerSpawnMin, RandomWaveBulletsPerSpawnMax + 1);
        float delayBetweenBullets = Random.Range(RandomWaveDelayBetweenBulletsMin, RandomWaveDelayBetweenBulletsMax);
        float delayBetweenSpawns = Random.Range(RandomWaveDelayBetweenSpawnsMin, RandomWaveDelayBetweenSpawnsMax);

        int nSpawns = Random.Range(spawnsMin, spawnsMax);
        int[] spawnindexArray = new int[nSpawns];
        for (int i = 0; i < nSpawns; i++)
        {
            spawnindexArray[i] = Random.Range(0, spawnerList.Count);
        }

        randomWave.bulletPrefab = prefab;
        randomWave.numberBulletsPerSpawn = numberBulletsPerSpawn;
        randomWave.delayBetweenBullets = delayBetweenBullets;
        randomWave.delayBetweenSpawns = delayBetweenSpawns;
        randomWave.spawnindexArray = spawnindexArray;

        if (randomWave == null)
        {
            Debug.LogError("Could not Spawn Wave");
        }
        else
        {
            StartCoroutine(SpawnWave(randomWave));
        }

    }

    public void SpawnWaveFromString(string inputString)
    {
        Debug.Log("Trying to Spawn from String: " + inputString);
        if (inputString == null)
        {
            Debug.LogError("Spawn inputstring is null!!!");
            return;
        }
        if (STRAIGHTWAVE.Equals(inputString))
        {
            SpawnRandomWaveFromParametersUsingPrefab(straightBulletPrefab);
        }
        else if (SPINWAVE.Equals(inputString))
        {
            SpawnRandomWaveFromParametersUsingPrefab(spinBulletPrefab);
        }
        else if (ZIGWAVE.Equals(inputString))
        {
            SpawnRandomWaveFromParametersUsingPrefab(zigzagBulletPrefab);
        }
        else
        {
            for (int i = 0; i < wavePatterns.Count; i++)
            {
                string patternTag = wavePatterns[i].tag;
                Debug.Log(patternTag);
                if (inputString.Equals(patternTag))
                {
                    StartCoroutine(SpawnWave(wavePatterns[i]));
                    break;
                }
            }
        }
    }
}
