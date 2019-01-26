using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnController : MonoBehaviour
{
    public Transform bulletPrefab;
    public Transform spawnPosition;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(SpawnWave(3, spawnPosition, (float)0.3));
        }
    }

    IEnumerator SpawnWave(int nBullets, Transform spawnPosition, float timeBetweenBullets)
    {
        for (int i = 0; i < nBullets; i++)
        {
            Instantiate(bulletPrefab, spawnPosition.position, spawnPosition.rotation);
            yield return new WaitForSeconds(timeBetweenBullets);
        }

    }

}
