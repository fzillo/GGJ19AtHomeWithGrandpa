using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnController : MonoBehaviour
{
    public Transform straightBulletPrefab;
    public Transform spinBulletPrefab;
    public List<Transform> spawnerList;
    public List<Wave> wavePatterns;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Wave w = SelectRandomWaveFromPool();
            if (w == null) return;
            StartCoroutine(SpawnWave(w));
        }
    }

    IEnumerator SpawnWave(Wave w)
    {
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

    public Wave SelectRandomWaveFromPool()
    {
        if (wavePatterns.Count == 0) return null;

        int index;
        index = Random.Range(0, wavePatterns.Count);

        Debug.Log("Random Wave index" + index + "/" + (wavePatterns.Count));
        return wavePatterns[index];
    }
}
