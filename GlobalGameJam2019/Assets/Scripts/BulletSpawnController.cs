using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnController : MonoBehaviour
{
    public Transform straightBulletPrefab;
    public Transform spinBulletPrefab;
    public List<Transform> spawnerList;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //StartCoroutine(SpawnWave(3, spinBulletPrefab, spawnerList[0], (float)0.3));
            Wave w = new Wave(straightBulletPrefab, 1, 1, new int[] { 1 }, 0);
            StartCoroutine(SpawnWave(w));
        }
    }

    [System.Obsolete("This is an obsolete method")]
    IEnumerator SpawnWave(int nBullets, Transform bulletPrefab, Transform spawnPosition, float timeBetweenBullets)
    {
        for (int i = 0; i < nBullets; i++)
        {
            Instantiate(bulletPrefab, spawnPosition.position, spawnPosition.rotation);
            yield return new WaitForSeconds(timeBetweenBullets);
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
                Instantiate(w.bulletPrefab, spawnerList[i].position, spawnerList[i].rotation);
                yield return new WaitForSeconds(w.delayBetweenSpawns);
            }
        }
    }

}
