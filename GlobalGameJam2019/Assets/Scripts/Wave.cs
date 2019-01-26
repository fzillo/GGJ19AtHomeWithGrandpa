using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public Transform bulletPrefab;
    public int numberBulletsPerSpawn = 1;
    public float delayBetweenBullets = 1;
    public int[] spawnindexArray;
    public float delayBetweenSpawns = 0;

    public Wave(Transform bulletPrefab, int numberBulletsPerSpawn,
     float delayBetweenBullets,
     int[] spawnindexArray, float delayBetweenSpawns)
    {
        this.bulletPrefab = bulletPrefab;
        this.numberBulletsPerSpawn = numberBulletsPerSpawn;
        this.delayBetweenBullets = delayBetweenBullets;
        this.spawnindexArray = spawnindexArray;
        this.delayBetweenSpawns = delayBetweenSpawns;
    }
}
